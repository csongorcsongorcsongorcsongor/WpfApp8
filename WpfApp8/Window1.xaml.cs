using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp8
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ServerConnection connection;

        public Window1(ServerConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            start();
        }
        void start()
        {
            asd();
        }
        async void create(object s, EventArgs e)
        {
            bool temp = await connection.createPerson(NameInput.Text, Convert.ToInt32(AgeInput.Text));
            if (temp)
            {
                MessageBox.Show("Sikeres létrehozás");
                asd();
            }
        }
        async void deleteAll(object s, EventArgs e)
        {
            bool temp = await connection.deleteAll();
            if (temp)
            {
                MessageBox.Show("Sikeres törlés");
                asd();
            }
        }
        async void asd()
        {

            NameStackPanel.Children.Clear();
            AgeStackPanel.Children.Clear();
            RemoveStackPanel.Children.Clear();
            List<string> allnames = await connection.AllNames();
            foreach (string item in allnames)
            {
                TextBlock namelabel = new TextBlock();
                namelabel.Text = item;
                NameStackPanel.Children.Add(namelabel);
                Button removebtn = new Button();
                removebtn.Content = "X";
                removebtn.Height = namelabel.Height;
                removebtn.Click += async (s, e) =>
                {
                    bool temp = await connection.deletePerson(namelabel.Text);
                    if (temp)
                    {
                        MessageBox.Show("Sikeres törlés");
                        asd();

                    }
                };
                RemoveStackPanel.Children.Add(removebtn);
            }
            List<string> allages = await connection.AllAges();
            foreach (string item in allages)
            {
                AgeStackPanel.Children.Add(new TextBlock() { Text = item });
                
            }

            

        }
    }
}
