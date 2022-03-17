using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.DTO
{
    
        public class ShopingDTO
        {
            public int BillNo { get; set; }
            public string CostomerName { get; set; }
            public string Date { get; set; }
            public decimal Tax { get; set; }
            public decimal Total { get; set; }
            public List<ItemDTO> ItemDTOs { get; set; }


        }
        public class ItemDTO
        {
            public int BillNo { get; set; }
            public string ItemNo { get; set; }
            public string ItemName { get; set; }
            public decimal Price { get; set; }
            public decimal Tax { get; set; }
            public string QTY { get; set; }
            public decimal TotalAmmount { get; set; }
        }
    }

