using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using To_do.Framework.Models;

namespace To_do.Framework
{
    public partial class ViewForm : Form
    {
        private int Id;
        private bool success;
        private int index;

        public ViewForm()
        {
            InitializeComponent();
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_To_doDataSet.Events' table. You can move, or remove it, as needed.
            this.eventsTableAdapter.Fill(this._To_doDataSet.Events);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (ToDoEntities toDoEntities = new ToDoEntities()) //Con esto hago uso del contexto. Para esto hay que ponera la referencia en donde se encutra el modelo.
            {//Ahora tengo que decirle los datos que se instertaran en la tabla llamada Events
                
                Event @event = new Event();     //Event = modelo
                @event.Event1 = txtEvent.Text;
                @event.Date = dtpDate.Value;
                @event.Hour = TimeSpan.Parse(txtHour.Text);
                @event.Address = txtAddress.Text;


                toDoEntities.Events.Add(@event); //Añado el objeto a la entidad
                toDoEntities.SaveChanges(); //Guardo los cambios nuevos de la entidad en la base de datos

                txtEvent.Clear();
                txtAddress.Clear();
                txtHour.Clear();
                
            }

        }

        private void dgvToDo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool success = int.TryParse((dgvToDo.SelectedCells[0].Value.ToString()), out Id);

            DataGridViewRow row = dgvToDo.Rows[index];

            txtEvent.Text = row.Cells[1].Value.ToString();
            dtpDate.Value = DateTime.Parse(row.Cells[2].Value.ToString());
            txtHour.Text = row.Cells[3].Value.ToString();
            txtAddress.Text = row.Cells[4].Value.ToString();


        }

        private void btnEdit_Click(object sender, EventArgs e) //https://chat.openai.com/share/5b7e380c-dfab-4e65-93de-65390f224573
        {
            using (ToDoEntities toDoEntities = new ToDoEntities())
            {
                Event @event = new Event();
                
                var eventUpdate = toDoEntities.Events.FirstOrDefault(p => p.Id == Id);

                eventUpdate.Event1 = txtEvent.Text;
                eventUpdate.Date = dtpDate.Value;
                eventUpdate.Address = txtAddress.Text;
                eventUpdate.Hour = TimeSpan.Parse(txtHour.Text);

                toDoEntities.SaveChanges();
            }
        }
    }
}
