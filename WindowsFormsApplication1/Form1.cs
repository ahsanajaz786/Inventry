using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.DTO;
using WindowsFormsApplication1.Interfaces;
using WindowsFormsApplication1.Services;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List<ItemDTO> items = new List<ItemDTO>();
        private readonly IShopping _shopping;
        public Form1()
        {
            InitializeComponent();
            _shopping = new ShoppingService();
            FillDataTable();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal price = 0;
            decimal tax = 0;
            int qty = 0;
            if (txt_itemName.Text == "" || txt_price.Text == "" || txt_qty.Text == "")
            {
                MessageBox.Show("Please Enter Info", "Error");
                return;
            }
            decimal.TryParse(txt_price.Text, out price);
            decimal.TryParse(txt_tax.Text, out tax);
            int.TryParse(txt_qty.Text, out qty);
            decimal txt = (price * qty);
            decimal finalTax = (tax * txt) / 100;

            ItemDTO itemDTO = new ItemDTO()
            {
                ItemNo = txt_itemNo.Text,
                ItemName = txt_itemName.Text,
                Price = price,
                Tax = finalTax
                ,
                TotalAmmount = price * qty,
                QTY = txt_qty.Text

            };
            items.Add(itemDTO);
            FillDataTable();
            clear();
            var total = items.Sum(o => o.TotalAmmount);
            var taxAmt = items.Sum(o => o.Tax);
            txt_total.Text = (taxAmt + total).ToString();

        }
        private void clear()
        {
            txt_itemNo.Text = "";
            txt_itemName.Text = "";
            txt_qty.Text = "";
            txt_price.Text = "";



        }
        private void FillDataTable()
        {
            BindingSource binding = new BindingSource();
            binding.DataSource = items;

            //bind datagridview to binding source
            dataGridView1.DataSource = binding;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (items.Count == 0)
            {
                MessageBox.Show("Error", "Please Add Items");
                return;
            }
            DateTime datetime;
            DateTime.TryParse(txt_date.Text,out datetime);
            decimal totalAmount = 0;
            decimal totalTax = 0;
            totalAmount = items.Sum(o => o.TotalAmmount);
            totalTax = items.Sum(o => o.Tax);
            var shoping = new ShopingDTO()
            {
                CostomerName = txt_costomer.Text,
                Tax = totalTax,
                Total = totalAmount,
                ItemDTOs = items,
                Date = datetime.ToString()

            };
            var res = _shopping.AddShopindCart(shoping);
            if (res)
            {
                clear();
                txt_costomer.Text = "";
                txt_date.Text = "";
                txt_tax.Text = "";
                items.Clear();
                FillDataTable();
                MessageBox.Show("Item Added", "Success");



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Main m = new Main();
            m.Show();
        }
    }
}
