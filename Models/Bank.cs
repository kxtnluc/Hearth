using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hearth.Models
{
    public class Bank
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public string Institution_Name { get; set; }
        public string Access_Token { get; set; } = string.Empty;
        public string? Request_Id { get; set; }
        public string Generated_Color
        {
            get
            {
                var color = "";

                switch (Institution_Name)
                {
                    case "First Horizon Bank - Digital Banking":
                        color = "#0050b5"; // Ripped from Website
                        break;
                    case "Discover":
                        color = "#C85A22"; // Ripped from Website
                        break;
                    case "Robinhood":
                        color = "#3B5B4B";
                        break;
                }

                return color;
            }
        }
    }
}
