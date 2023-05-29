using CarShop.Models.Entities;
using System.Diagnostics;

namespace CarShop.WpfClient.Models
{
    public class BrandModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BrandModel()
        {
        }

        public BrandModel(int id, string name)
        {
            Id = id;
            Name = name;
        }


    public BrandModel(BrandModel other)
    {
      Id = other.Id;
      Name = other.Name;
    }
  }
}
