using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using web_szolgaltatas.Entities;
using web_szolgaltatas.MNBServiceReference;

namespace web_szolgaltatas
{
    public partial class Form1 : Form
    {
        BindingList<RateDate> _rates = new BindingList<RateDate>();
        BindingList<string> _currencies = new BindingList<string>();


        public Form1()
        {
            InitializeComponent();
            loadCurrencyXml(getCurrencies());
            comboBox1.DataSource = _currencies;
            RefreshData();
                  
        }

        private void RefreshData()
        {
            if (comboBox1.SelectedItem is null) return;
            _rates.Clear();
            loadXml(getRates());
            dataGridView1.DataSource = _rates;
            makechart();
            
        }

        private void makechart()
        {
            chart1.DataSource = _rates;
            Series sorozatok = chart1.Series[0];
            sorozatok.ChartType = SeriesChartType.Line;
            sorozatok.XValueMember = "Date";
            sorozatok.YValueMembers = "Value";

            var jelmagyarazat = chart1.Legends[0];
            jelmagyarazat.Enabled = false;

            var diagramterulet = chart1.ChartAreas[0];
            diagramterulet.AxisY.IsStartedFromZero = false;
            diagramterulet.AxisY.MajorGrid.Enabled = false;
            diagramterulet.AxisX.MajorGrid.Enabled = false;


        }

        private void loadXml(string xmlstring)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);
            foreach (XmlElement item in xml.DocumentElement)
            {
                RateDate r = new RateDate();
                r.Date = DateTime.Parse(item.GetAttribute("date"));
                var childElement = (XmlElement)item.ChildNodes[0];
                if (childElement != null)
                {

                
                r.Currency = childElement.GetAttribute("curr");
                decimal unit = decimal.Parse(childElement.GetAttribute("unit"));
                r.Value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                {
                    r.Value = r.Value / unit;
                }
                _rates.Add(r);
                }
            }
        }

        private string getRates()
        {
            
            var mnbService = new MNBServiceReference.MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody();
            request.currencyNames = comboBox1.SelectedItem.ToString();// "EUR";
            request.startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");//"2020-01-01";
            request.endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");//"2020-06-30";
            var response = mnbService.GetExchangeRates(request);
            return response.GetExchangeRatesResult;


        }
        private void loadCurrencyXml(string xmlstring)
        {
            _currencies.Clear();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);
            foreach (XmlElement item in xml.DocumentElement.ChildNodes[0])
            {
                _currencies.Add(item.InnerText);
            }
        }
        private string getCurrencies()
        {

            var mnbService = new MNBServiceReference.MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody();
            var resp = mnbService.GetCurrencies(request);
            return resp.GetCurrenciesResult;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
