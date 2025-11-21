using Hearth.Models;
using ApexCharts;
using Hearth.Models;
using Hearth.Services;
using Microsoft.AspNetCore.Components;

namespace Hearth.Components.Pages.Finances.Subpages
{
    public partial class CategorizationPage
    {
        private bool initialRenderComplete = false;

        private Category selectedCategory;
        private List<CategoryOrganizationRule> organizationRules = new List<CategoryOrganizationRule>();
        private List<Category> categories = new List<Category>();
        private int[] excludeColumns = [0, 3, 4, 5, 6,];
        protected override async Task OnInitializedAsync()
        {

            try
            {
                categories = CategoryFunctions.GetAll(_dbContext, true);
                organizationRules = CategoryOrganizationRuleFunctions.GetAll(_dbContext);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            await UpdateBarGraph();

        }

        private class CategoryBar
        {
            public string Label { get; set; }
            public int NumberOfTransactions { get; set; }
        }
        private ApexChartOptions<CategoryBar> options = new ApexChartOptions<CategoryBar>();
        private List<CategoryBar> categoriesBar = new List<CategoryBar>();

        // this doesn't really work
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                initialRenderComplete = true;
                StateHasChanged(); // Trigger re-render to hide the spinner
            }
        }

        private Task UpdateBarGraph()
        {
            options = new ApexChartOptions<CategoryBar>
            {
                PlotOptions = new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        Horizontal = true
                    }
                }
            };
            foreach (var category in categories)
            {
                if (category.Transactions == null)
                {
                    categoriesBar.Add(new CategoryBar
                    {
                        Label = category.Name,
                        NumberOfTransactions = 0,
                    });
                }
                else
                {
                    categoriesBar.Add(new CategoryBar
                    {
                        Label = category.Name,
                        NumberOfTransactions = category.Transactions.Count(),
                    });
                }
            }

            return Task.CompletedTask;
        }

        private Task HandleCategorySelection(SelectedData<CategoryBar> selected)
        {
            //var label = selected.Series[selected.DataPointIndex].X.ToString();

            //selectedCategory = categories.FirstOrDefault(c => c.Name == label);

            return Task.CompletedTask;
        }
        // Manage Category Modal -----------------------------------------------------
        private bool showModal;

        private void OpenModal()
        {
            showModal = true;
        }

        private void CloseModal()
        {
            showModal = false;
        }

        // Add Modal -----------------------------------------------------------

        private bool showAddModal;

        private void OpenAddModal()
        {
            showAddModal = true;
        }

        private void CloseAddModal()
        {
            showAddModal = false;
        }

        private async void OnAddModalChanged(bool value)
        {
            showAddModal = value;
            if(showAddModal == false)
            {
                await RefreshData();
            }
            return;
        }


        // Edit Modal -----------------------------------------------------------
        private async void OnEditModalChanged(bool value)
        {
            showEditModal = value;
            if (showEditModal == false)
            {
                await RefreshData();
            }
            return;
        }

        private bool showEditModal;

        private void OpenEditModal(Category category)
        {
            selectedCategory = category;
            showEditModal = true;
        }

        private void CloseEditModal()
        {
            showEditModal = false;
        }

        // Delete Modal -----------------------------------------------------------

        private bool showDeleteModal;

        private void OpenDeleteModal(Category category)
        {
            selectedCategory = category;
            showDeleteModal = true;
        }

        private void CloseDeleteModal()
        {
            showDeleteModal = false;
        }

        private async void DeleteCategory(int categoryId)
        {
            CategoryFunctions.Remove(_dbContext, categoryId);
            CloseDeleteModal();
            await RefreshData();
        }

        private async void HandleRuleAssignBtn()
        {
            TransactionFunctions.AutoAssignAllCategories(_dbContext);
            await RefreshData();
        }

        // Refresh ---
        private async Task RefreshData()
        {
            categories = CategoryFunctions.GetAll(_dbContext, true);
            organizationRules = CategoryOrganizationRuleFunctions.GetAll(_dbContext);

            categoriesBar.Clear();
            await UpdateBarGraph();

            StateHasChanged();
        }
    }
}