using InventoryManagementModule.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementModule.Controllers
{
    public class ProductController : Controller
    {
        string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventoryDB;Integrated Security=True";
        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Products";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        Price = Convert.ToDecimal(dr["Price"])
                    });
                }
            }

            return View(products);
        }
        public ActionResult Create()
        {
            return View();
        }

        // CREATE: Save product
        [HttpPost]
        public ActionResult Create(Product p)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"INSERT INTO Products
                    (ProductName, Quantity, Price)
                    VALUES (@ProductName, @Quantity, @Price)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@ProductName", p.ProductName);

                cmd.Parameters.AddWithValue(
                    "@Quantity", p.Quantity);

                cmd.Parameters.AddWithValue(
                    "@Price", p.Price);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // UPDATE: Display existing product
        public ActionResult Edit(int id)
        {
            Product p = new Product();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query =
                    "SELECT * FROM Products WHERE ProductId=@ProductId";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductId", id);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    p.ProductId = Convert.ToInt32(dr["ProductId"]);
                    p.ProductName = dr["ProductName"].ToString();
                    p.Quantity = Convert.ToInt32(dr["Quantity"]);
                    p.Price = Convert.ToDecimal(dr["Price"]);
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return View(p);
        }

        // UPDATE: Save changes
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"UPDATE Products SET
                    ProductName=@ProductName,
                    Quantity=@Quantity,
                    Price=@Price
                    WHERE ProductId=@ProductId";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@ProductId", p.ProductId);

                cmd.Parameters.AddWithValue(
                    "@ProductName", p.ProductName);

                cmd.Parameters.AddWithValue(
                    "@Quantity", p.Quantity);

                cmd.Parameters.AddWithValue(
                    "@Price", p.Price);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // DELETE: Remove product
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query =
                    "DELETE FROM Products WHERE ProductId=@ProductId";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}