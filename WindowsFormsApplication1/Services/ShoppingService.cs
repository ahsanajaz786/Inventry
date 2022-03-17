using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.DTO;
using WindowsFormsApplication1.Interfaces;

namespace WindowsFormsApplication1.Services
{
    public class ShoppingService : IShopping
    {
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        string conn = "Data Source=DESKTOP-7C7BH2I;Initial Catalog=shopping;Integrated Security=True";
        public ShoppingService()
        {
            sqlCon = new SqlConnection(conn);
            sqlCon.Open();
        }

        public bool AddShopindCart(ShopingDTO shopingDTO)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                sqlCmd = new SqlCommand("INSERT_IN_SALEMASTER", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@date", shopingDTO.Date);
                sqlCmd.Parameters.AddWithValue("@customer", shopingDTO.CostomerName);
                sqlCmd.Parameters.AddWithValue("@tax", shopingDTO.Tax);
                sqlCmd.Parameters.AddWithValue("@total", shopingDTO.Total + shopingDTO.Tax);
                sqlCmd.Parameters.Add("@id", SqlDbType.Int);
                sqlCmd.Parameters["@id"].Direction = ParameterDirection.Output;
                int rdr = sqlCmd.ExecuteNonQuery();
                var id = sqlCmd.Parameters["@id"].Value.ToString();
                int billNo = 0;
                int.TryParse(id, out billNo);

                sqlCon.Close();
                foreach (var item in shopingDTO.ItemDTOs)
                {
                    item.BillNo = billNo;
                    AddSaleDetail(item);

                }

                return true;
            }
            catch (Exception l)
            {
                return false;
            }
        }
        private int AddSaleDetail(ItemDTO itemDTOs)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            sqlCmd = new SqlCommand("INSERT_INTO_SALEDETAIL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@billno", itemDTOs.BillNo);
            sqlCmd.Parameters.AddWithValue("@itemNo", itemDTOs.ItemNo);
            sqlCmd.Parameters.AddWithValue("@itemName", itemDTOs.ItemName);
            sqlCmd.Parameters.AddWithValue("@price", itemDTOs.Price);
            sqlCmd.Parameters.AddWithValue("@qty", itemDTOs.QTY);
            sqlCmd.Parameters.AddWithValue("@tax", itemDTOs.Tax);

            int rdr = sqlCmd.ExecuteNonQuery();
            return rdr;


        }


        public SqlDataAdapter LoadAllSale(string datefrom, string dateto)
        {
            sqlCmd = new SqlCommand("GET_ALL_SALES", sqlCon);

            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@endDate", dateto);
            sqlCmd.Parameters.AddWithValue("@startdate", datefrom);

            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
            return sda;

        }
    }
}
