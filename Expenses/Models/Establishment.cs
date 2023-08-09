﻿namespace Expenses.Models
{
    public class Establishment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }

        public Establishment()
        {
        }

        public Establishment(int id, string name, string description, Category category, SubCategory subCategory)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            SubCategory = subCategory;
        }

        public Establishment(int id, string name, string description, Category category)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
        }

        public Establishment(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
