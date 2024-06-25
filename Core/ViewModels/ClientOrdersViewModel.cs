using ToGoodToGo.Core.Models.Domains;

namespace ToGoodToGo.Core.ViewModels
{
    public class ClientOrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
