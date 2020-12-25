using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Exercise2_071_AisyahDiniaKencana
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TampilData();
        }

        public class Mahasiswa
        {
            private string _nama, _nim, _prodi, _angkatan;
            public string nama
            {
                get { return _nama; }
                set { _nama = value; }
            }

            public string nim
            {
                get { return _nim; }
                set { _nim = value; }
            }

            public string prodi
            {
                get { return _prodi; }
                set { _prodi = value; }
            }

            public string angkatan
            {
                get { return _angkatan; }
                set { _angkatan = value; }
            }
        }

        public void TampilData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            dataGridView1.DataSource = data;

        }

        public void SearchData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            string nim = tbNIM.Text;
            if (nim == null || nim == "")
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                var item = data.Where(x => x.nim == tbNIM.Text).ToList();

                dataGridView1.DataSource = item;
            }
        }

        string baseurl = "http://localhost:1907/";
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Mahasiswa mhs = new Mahasiswa();
            mhs.nama = tbNama.Text;
            mhs.nim = tbNIM.Text;
            mhs.prodi = tbProdi.Text;
            mhs.angkatan = tbAngkatan.Text;

            var data = JsonConvert.SerializeObject(mhs);
            var postdata = new WebClient();
            postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = postdata.UploadString(baseurl + "Mahasiswa", data);
            Console.WriteLine(response);
            TampilData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            string nim = tbNIM.Text;
            var item = data.Where(x => x.nim == tbNIM.Text).FirstOrDefault();
            if (item != null)
            {
                // update logger with your textboxes data
                item.nama = tbNama.Text;
                item.nim = tbNIM.Text;
                item.prodi = tbProdi.Text;
                item.angkatan = tbAngkatan.Text;

                // Save everything
                string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                var postdata = new WebClient();
                postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = postdata.UploadString(baseurl + "UpdateMahasiswa", output);
                Console.WriteLine(response);
                TampilData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            if (MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string nim = tbNIM.Text;
                var item = data.Where(x => x.nim == tbNIM.Text).FirstOrDefault();
                if (item != null)
                {
                    data.Remove(item);
                    // Save everything
                    string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                    var postdata = new WebClient();
                    postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string response = postdata.UploadString(baseurl + "DeleteMahasiswa", output);
                    Console.WriteLine(response);
                    TampilData();
                }

            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            TampilData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbNama.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            tbNIM.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            tbProdi.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            tbAngkatan.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
        }
    }
}
