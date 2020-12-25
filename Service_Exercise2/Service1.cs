using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service_Exercise2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";
            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-H7JUHR5; Initial Catalog=\"TI UMY\"; Persist Security Info=True; User ID=sa; Password=102314");
            string query = string.Format("Insert into dbo.Mahasiswa values ('{0}','{1}','{2}','{3}')", mhs.nama, mhs.nim, mhs.prodi, mhs.angkatan);
            SqlCommand sqlcom = new SqlCommand(query, sqlcon); //yang  dikirim ke sql

            try
            {
                sqlcon.Open(); //membuka connection sql

                Console.WriteLine(query);

                sqlcom.ExecuteNonQuery(); //mengeksekusi untuk memasukkan data

                sqlcon.Close();

                msg = "Sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }
            return msg;
        }

        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> mahas = new List<Mahasiswa>();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-H7JUHR5; Initial Catalog=\"TI UMY\"; Persist Security Info=True; User ID=sa; Password=102314");
            string query = "select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa";
            SqlCommand com = new SqlCommand(query, con);

            try
            {
                con.Open(); //membuka connection sql
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = reader.GetString(0);
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                    mahas.Add(mhs);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mahas;
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-H7JUHR5; Initial Catalog=\"TI UMY\"; Persist Security Info=True; User ID=sa; Password=102314");
            string query = string.Format("select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand com = new SqlCommand(query, con);

            try
            {
                con.Open(); //membuka connection sql
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    mhs.nama = reader.GetString(0);
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mhs;

        }

        public string DeleteMahasiswa(string nim)
        {
            string msg = "GAGAL";
            SqlConnection con2 = new SqlConnection("Data Source = DESKTOP - H7JUHR5; Initial Catalog =\"TI UMY\"; Persist Security Info=True; User ID=sa; Password=102314");
            string query = String.Format("Delete from dbo.Mahasiswa where NIM = '{  0}  '", nim);
            SqlCommand cmd = new SqlCommand(query, con2); //yang dikirim ke sql 
            try
            {
                con2.Open(); //membuka connection sql Console.WriteLine(query); cmd.ExecuteNonQuery(); //mengeksekusi untuk memasukkan data con2.Close(); msg = "Sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }
            return msg;
        }
        public string UpdateMahasiswa(string nim, string nama)
        {
            string msg = "GAGAL";
            SqlConnection conn = new SqlConnection("Data Source = DESKTOP - H7JUHR5; Initial Catalog =\"TI UMY\"; Persist Security Info=True; User ID=sa; Password=102314");
            string query = "update dbo.Mahasiswa set Nama = '" + nama + "'" +
            " where NIM = '" + nim + "' ";
            SqlCommand cmd2 = new SqlCommand(query, conn); //yang dikirim ke sql 
            try
            {
                conn.Open(); //membuka connection sql Console.WriteLine(query); cmd2.ExecuteNonQuery(); //mengeksekusi untuk memasukkan data conn.Close(); msg = "Sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }
            return msg;
        }
    }
}
