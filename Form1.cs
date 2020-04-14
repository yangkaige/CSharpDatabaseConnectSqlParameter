using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonInfoApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            string sql = "insert into TblPerson values(@UserName,@Gender,@Birthday)";
            SqlParameter[] sqlParameters = new SqlParameter[] { 
                new SqlParameter("@UserName",SqlDbType.VarChar,50) {
                    Value = txtName.Text
                },
                new SqlParameter("@Gender",SqlDbType.VarChar,20) {
                    Value = cboGender.Text
                },
                new SqlParameter("@Birthday",SqlDbType.VarChar,20) {
                    Value = dtpBirth.Value
                }
            };
            AddOrEdit(sql, sqlParameters);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择一条记录");
                return;
            }
            string sql = "update TblPerson set UserName=@UserName,Gender=@Gender,Birthday=@Birthday where pId=@pId";
            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("@UserName",SqlDbType.VarChar,50) {
                    Value = txtName.Text
                },
                new SqlParameter("@Gender",SqlDbType.VarChar,20) {
                    Value = cboGender.Text
                },
                new SqlParameter("@Birthday",SqlDbType.VarChar,20) {
                    Value = dtpBirth.Value
                },
                new SqlParameter("@pId",SqlDbType.Int,8) {
                    Value = lblUserId.Text
                }
            };
            AddOrEdit(sql, sqlParameters);
        }
        /// <summary>
        /// 新增或编辑逻辑
        /// </summary>
        /// <param name="sql"></param>
        private void AddOrEdit(string sql,SqlParameter[] sqlParameters)
        {

            int r = SqlHelper.ExecuteNonQuery(sql, sqlParameters);
            if(r > 0)
            {
                MessageBox.Show("操作成功！！");
                LoadData();
            }
            else
            {
                MessageBox.Show("操作失败！！");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        
        /// <summary>
        /// 从数据库中加载数据
        /// </summary>
        private void LoadData()
        {
            
            bool flag = true;
            List<Person> list = new List<Person>();
            
            using (SqlDataReader reader = SqlHelper.ExecuteReader("select UserName,Gender,Birthday,pId from TblPerson"))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.IsDBNull(0) ? null : (string)reader.GetString(0);
                        string gender = reader.IsDBNull(1) ? null : (string)reader.GetString(1);
                        string birthday = reader.IsDBNull(2) ? null : (string)reader.GetString(2);
                        int pId = reader.GetInt32(3);

                        list.Add(new Person()
                        {
                            UserName = name,
                            Gender = gender,
                            Birthday = birthday,
                            pId = pId
                        });
                    }
                }
                else
                {
                    flag = false;
                }
                dataGridView.DataSource = list;
                dataGridView.ClearSelection();
            }
            
            if (!flag)
            {
                MessageBox.Show("无查询数据");
            }
        }
        /// <summary>
        /// 选中行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (list.Count <= 0)
            //{
            //    return;
            //}
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            Person p = row.DataBoundItem as Person;

            txtName.Text = p.UserName;
            cboGender.Text = p.Gender.Trim();
            dtpBirth.Value = Convert.ToDateTime(p.Birthday);
            lblUserId.Text = p.pId.ToString();
        }
    }
}
