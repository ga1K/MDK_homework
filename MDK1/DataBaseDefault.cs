using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MDK1;


public class DatabaseDefault
{
    private string connectionString = "Server=localhost;Database=MDK_DB;Trusted_Connection=True;";

    public List<Partner> GetPartnersWithSales()
    {
        List<Partner> partners = new List<Partner>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Запрос, который объединяет таблицы Partner_Products_import и Partners_import
            string query = @"
                SELECT
p.PartnerType,
    p.PartnerName,
    p.Phone,
    p.Rating
FROM 
    dbo.Partners p
GROUP BY 
    p.PartnerName, p.PartnerType, p.Phone, p.Rating;
";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Partner partner = new Partner
                {
                    PartnerType = reader["PartnerType"].ToString(),
                    Name = reader["PartnerName"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Rating = reader["Rating"].ToString()

                };

                // Расчет скидки на основе объема продаж

                partners.Add(partner);
            }
        }

        return partners;
    }

}