using GalaSoft.MvvmLight;

namespace CarShop.WpfClient.Models
{
  public class CarModel : ObservableObject
  {
    private int id;

    public int Id
    {
      get { return id; }
      set { Set(ref id, value); }
    }

    private int price;

    public int Price
    {
      get { return price; }
      set { Set(ref price, value); }
    }

    private string model;

    public string Model
    {
      get { return model; }
      set { Set(ref model, value); }
    }

    private int brandId;

    public int BrandId
    {
      get { return brandId; }
      set { Set(ref brandId, value); }
    }

    public CarModel()
    {
    }

    public CarModel(int id, int price, string model, int brandId)
    {
      this.id = id;
      this.price = price;
      this.model = model;
      this.brandId = brandId;
    }

    public CarModel(CarModel other)
    {
      id = other.Id;
      brandId = other.BrandId;
      model = other.Model;
      price = other.Price;
    }

    public override string ToString()
    {
      return $"Id: {id}, Model: {model}, Price: {price}, BrandId: {brandId}";
    }
  }
}
