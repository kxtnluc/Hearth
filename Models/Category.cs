using Hearth.Data;
using Hearth.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearth.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Is_Need { get; set; }
        public string? Description { get; set; }
        public decimal? Weight { get; set; }
        public int? Example_Transaction_Id { get; set; }
        //[NotMapped]
        //public Transaction Example_Transaction { get; set; }
        public string Hex_Color { get; set; } = "var(--info-color)";
        public bool Ignore { get; set; } = false;
        public bool Income { get; set; } = false;
        public List<Transaction>? Transactions { get; set; }
        public List<CategoryOrganizationRule> OrganizationRules { get; set; }
    }

    public class CategoryFunctions
    {
        public static List<Category> GetAll(HearthDbContext _dbContext, bool includeTransactions = false)
        {
            List<Category> result = new List<Category>();
            if (includeTransactions)
            {
                var test = _dbContext.Transactions
                    .Select(t => new { t.Id, t.Authorized_Datetime })
                    .ToList();
                try
                {
                    result = _dbContext.Categories.Include(c => c.Transactions).ToList();
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                result = _dbContext.Categories.ToList();
            }


            return result;
        }
        public static void Add(HearthDbContext _dbContext, Category categoryToAdd)
        {

            var foundIdenticalCategory = _dbContext.Categories.FirstOrDefault(c => c.Name == categoryToAdd.Name) ?? null;

            if (
                    categoryToAdd != null &&
                    !string.IsNullOrEmpty(categoryToAdd.Name) &&
                    foundIdenticalCategory == null
            )
            {
                _dbContext.Categories.Add(categoryToAdd);
                _dbContext.SaveChanges();
            }

            return;
        }

        public static void Remove(HearthDbContext _dbContext, int id)
        {

            var categoryToRemove = _dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryToRemove != null)
            {
                _dbContext.Categories.Remove(categoryToRemove);
                _dbContext.SaveChanges();
            }

            return;
        }

        public static void Update(HearthDbContext _dbContext, int id, Category categoryUpdates)
        {

            var categoryToUpdate = _dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = categoryUpdates.Name;
                categoryToUpdate.Description = categoryUpdates.Description;
                categoryToUpdate.Hex_Color = categoryUpdates.Hex_Color;
                categoryToUpdate.Income = categoryUpdates.Income;
                categoryToUpdate.Ignore = categoryUpdates.Ignore;
                _dbContext.SaveChanges();
            }
        }
    }

    // Rules ----------------------------------------------------------------------------------------------------
    // ----------------------------------------------------------------------------------------------------
    // -----------------------------------------------------------------------------------------------
    public class CategoryOrganizationRule
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        // What the rule sets to
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        // Criteria of the rule ---------------------
        public string? Plaid_Category { get; set; }
        public string? Plaid_Category_Detailed { get; set; }
        public STRING_OPERATOR? Merchant_Name_Operator { get; set; }
        public string? Merchant_Name { get; set; }
        public string? Transaction_Name { get; set; }
        public STRING_OPERATOR? Transaction_Name_Operator { get; set; }
        public decimal? Amount { get; set; }
        public OPERATOR? Amount_Operator { get; set; }
        public decimal? Give_Or_Take { get; set; }
        public bool Active { get; set; } = true;
        public string Name { get; set; }
    }



    public class CategoryOrganizationRuleFunctions
    {
        public static List<CategoryOrganizationRule> GetAll(HearthDbContext dbContext, bool includeCategory = false)
        {
            if (includeCategory)
            {
                return dbContext.CategoryOrganizationRules
                    .Include(cor => cor.Category)
                    .ToList();
            }

            return dbContext.CategoryOrganizationRules.ToList();
        }

        public static Task Add(HearthDbContext dbContext, CategoryOrganizationRule item)
        {
            if (item != null)
            {
                dbContext.CategoryOrganizationRules.Add(item);
                dbContext.SaveChanges();
            }

            return Task.CompletedTask;
        }

        public static Task Remove(HearthDbContext dbContext, CategoryOrganizationRule item)
        {
            if (item != null)
            {
                dbContext.CategoryOrganizationRules.Remove(item);
                dbContext.SaveChanges();
            }

            return Task.CompletedTask;
        }

        public static Task Update(HearthDbContext dbContext, CategoryOrganizationRule item)
        {
            if (item != null)
            {
                var foundRuleToUpdate = dbContext.CategoryOrganizationRules.SingleOrDefault(r => r.Id == item.Id);
                if (foundRuleToUpdate != null)
                {
                    foundRuleToUpdate.Name = item.Name;
                    foundRuleToUpdate.Merchant_Name = item.Merchant_Name;
                    foundRuleToUpdate.Amount = item.Amount;
                    foundRuleToUpdate.CategoryId = item.CategoryId;
                    foundRuleToUpdate.Plaid_Category = item.Plaid_Category;
                    foundRuleToUpdate.Active = item.Active;
                    foundRuleToUpdate.Amount_Operator = item.Amount_Operator;

                    dbContext.SaveChanges();
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
    }
    public enum OPERATOR
    {
        Equals = 0,
        GreaterThan = 1,
        LessThan = 2,
        GreaterThanOrEqualTo = 3,
        LessThanOrEqualTo = 4,
        Between = 5,
        GiveOrTake = 6,
    }

    public enum STRING_OPERATOR
    {
        Exact,
        StartsWith,
        EndsWith,
        Contains,
        Regex
    }

    public class CategoryUtils
    {


        public static string[] tags =
        [
            "BANK_FEES",
            "ENTERTAINMENT",
            "FOOD_AND_DRINK",
            "GENERAL_MERCHANDISE",
            "GENERAL_SERVICES",
            "GOVERNMENT_AND_NON_PROFIT",
            "HOME_IMPROVEMENT",
            "INCOME",
            "LOAN_PAYMENTS",
            "MEDICAL",
            "RENT_AND_UTILITIES",
            "TRANSFER_IN",
            "TRANSFER_OUT",
            "TRANSPORTATION",
            "TRAVEL"
        ];

        public static string[] operatorSigns =
        [
            "=",
            ">",
            "<",
            "≥",
            "≤",
            //"≥ ≤",
            //"+/-"
        ];

        public static string[] stringOperatorSigns = 
        [
            "Exact",
            "Starts With",
            "Ends With",
            "Contains",
            //"Resembles"

        ];

        public static string OperatorToString(OPERATOR? o, STRING_OPERATOR? s)
        {
            if(o != null)
            {
                switch (o)
                {
                    case OPERATOR.Equals:
                        return "=";
                    case OPERATOR.GreaterThan:
                        return ">";
                    case OPERATOR.LessThan:
                        return "<";
                    case OPERATOR.GreaterThanOrEqualTo:
                        return "≥";
                    case OPERATOR.LessThanOrEqualTo:
                        return "≤";
                    case OPERATOR.Between:
                        return "≥ ≤";
                    case OPERATOR.GiveOrTake:
                        return "+/-";
                    default:
                        return string.Empty;
                }
            }
            else if (s != null)
            {
                switch (s)
                {
                    case STRING_OPERATOR.Exact:
                        return "Exact";
                    case STRING_OPERATOR.StartsWith:
                        return "Starts With";
                    case STRING_OPERATOR.EndsWith:
                        return "Ends With";
                    case STRING_OPERATOR.Contains:
                        return "Contains";
                    case STRING_OPERATOR.Regex:
                        return "Resembles";
                    default:
                        return string.Empty;
                }
            }

            return string.Empty;
        }

        public static OPERATOR? StringToOperator(string input)
        {
            switch (input)
            {
                case "=":
                    return OPERATOR.Equals;
                case ">":
                    return OPERATOR.GreaterThan;
                case "<":
                    return OPERATOR.LessThan;
                case "≥":
                    return OPERATOR.GreaterThanOrEqualTo;
                case "≤":
                    return OPERATOR.LessThanOrEqualTo;
                case "≥ ≤":
                    return OPERATOR.Between;
                case "+/-":
                    return OPERATOR.GiveOrTake;
                default:
                    return null;
            }
        }

        public static STRING_OPERATOR? StringToStringOperator(string input)
        {
            switch (input)
            {
                case "Exact":
                    return STRING_OPERATOR.Exact;
                case "Starts With":
                    return STRING_OPERATOR.StartsWith;
                case "Ends With":
                    return STRING_OPERATOR.EndsWith;
                case "Contains":
                    return STRING_OPERATOR.Contains;
                case "Resembles":
                    return STRING_OPERATOR.Regex;
                default:
                    return null;
            }
        }

    }
}
