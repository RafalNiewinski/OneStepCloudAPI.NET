using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public string Name { get; set; }

        public static implicit operator int(ProductCategory cat) { return cat.Id; }
    }
}
