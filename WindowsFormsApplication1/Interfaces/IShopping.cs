using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.DTO;

namespace WindowsFormsApplication1.Interfaces
{
    public interface IShopping
    {
        bool AddShopindCart(ShopingDTO shopingDTO);
        SqlDataAdapter LoadAllSale(string datefrom, string dateto);
    }
}
