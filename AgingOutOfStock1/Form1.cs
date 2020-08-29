using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Documents;

namespace AgingOutOfStock1
{
    public partial class Form1 : Form
    {

        protected string curStrValue = "Null", newStrValue = "Null", whatToDo;
        protected bool curIsNumeric, newIsNumeric, curIsNull, newIsNull, noMoreDays;
        protected int curIntValue, newIntValue;
        protected List<int> arrQ = new List<int>();
        protected List<InventoryHistory> realInventory = new List<InventoryHistory>();
        protected List<cmpInventory> cmpRealInventory = new List<cmpInventory>();
        protected List<cmpInventory> cmpSimuInventory = new List<cmpInventory>();
        protected string opPOP = "";

        public Excel.Application xlApp;
        public Excel.Workbook xlWorkBook;
        public Excel.Worksheet xlWorkSheet;
        public Excel.Range range;
        object misvalue = System.Reflection.Missing.Value;

        string[,] WHSize = new string[35,2];

        class TimeBaseRule
        {   //1:T> 2:T<= 3:S> 4:S<= 5:S* (+) S/ (-) 6:S+
            public int T1;
            public int T2;
            public int S1;
            public int S2;
            public int MD; //Multiply or Division
            public int AD; //Addition or Deduction
        }

        class Model //General and TimeBased models
        {
            public int totalQ = 0;
            public int numPO = 0;
            public int DftN; //Top up OH
            public int Method; //Method to calculate PO Qty, 1=General, 2=Timebased
            public List<TimeBaseRule> TimeBase = new List<TimeBaseRule>();
            //1:model 2:Sold 3:Lose 4:UnitsPerDay 5:DaysOutStock 6:OrderTimes
            public Tuple<string, int, int, int, int, int> Result = new Tuple<string, int, int, int, int, int>("",0,0,0,0,0);
            public double Rating;
        }

        class RealPOETA
        {
            public List<Tuple<string>> realPO = new List<Tuple<string>>();
            public struct ETADay
            {
                public int num;
                public int eta;
            }
            public ETADay [] etaDay = new ETADay[7];
            public int avgETA;

            public void clearRealPO()
            {
                realPO.Clear();
                Array.Clear(etaDay,0,etaDay.Length);
            }
        }

        RealPOETA realPOETA = new RealPOETA();
        
        

        class Configuration
        {
            
            public List<Model> model = new List<Model>();
            
            //1:ETA 2:Q (Order Qty)
            public List<Tuple<string, int>> POInfo = new List<Tuple<string, int>>();
        }

        Configuration config = new Configuration();

        class ItemCode
        {
            public string itemcode;
            public string description;
            public string level;
            public double age;
            public double length;
            public double width;
            public double height;

        }
        List<ItemCode> itemCode = new List<ItemCode>();

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            loadWHSize();

            dataGridCondition.Rows.Add("End of Day", "OHnew = OHnew - Sold");
            dataGridCondition.Rows.Add("(sold before + sold today) >= OH/2", "Place PO; Get Order Qty; S = 0; T = 0");
            dataGridCondition.Rows.Add("OHnew = 0", "Place PO; Get Order Qty; S = 0; T = 0");
            dataGridCondition.Rows.Add("PO Receiving", "QR = Q; Q = 0");
            dataGridCondition.Rows.Add("Stock Receiving", "OH = OHnew = OHnew + Stock Receiving; S = 0 ; QR = 0");
            dataGridCondition.Rows.Add("Transfer Out (TO<OH-2S, T>48)", "N = N - TO; OH = OH - TO; OHnew = OHnew - TO; T = 0");
            dataGridCondition.Rows.Add("Transfer IN", "N = N + TI; OH = OH + TI; OHnew = OHnew + TI");


            dataGridAdjustQ.Rows.Add("", "7", "", "", "2", "");
            dataGridAdjustQ.Rows.Add("7", "13", "", "3", "", "1");
            dataGridAdjustQ.Rows.Add("7", "13", "3", "", "", "2");
            dataGridAdjustQ.Rows.Add("13", "20", "", "", "1", "");
            dataGridAdjustQ.Rows.Add("20", "30", "", "2", "", "-2");
            dataGridAdjustQ.Rows.Add("20", "30", "2", "", "", "-1");
            dataGridAdjustQ.Rows.Add("30", "", "", "", "-2", "");

            dataGridSmall.Rows.Add("1", "1", "2");
            dataGridSmall.Rows.Add("2", "3", "5");
            dataGridSmall.Rows.Add("4", "6", "10");
            dataGridSmall.Rows[0].HeaderCell.Value = "Low";
            dataGridSmall.Rows[1].HeaderCell.Value = "Medium";
            dataGridSmall.Rows[2].HeaderCell.Value = "High";



            dataGridLarge.Rows.Add("1", "1", "2");
            dataGridLarge.Rows.Add("2", "2", "4");
            dataGridLarge.Rows.Add("2", "4", "6");
            dataGridLarge.Rows[0].HeaderCell.Value = "Low";
            dataGridLarge.Rows[1].HeaderCell.Value = "Medium";
            dataGridLarge.Rows[2].HeaderCell.Value = "High";


            foreach (DataGridViewRow row in dataGridSmall.Rows)
            {
                row.Selected = false;
            }
            foreach (DataGridViewRow row in dataGridLarge.Rows)
            {
                row.Selected = false;
            }
            dataGridSmall.Rows[2].Cells[2].Selected = true;
            comboItem.SelectedIndex = 0;
            comboRR.SelectedIndex = 2;
            comboWH.SelectedIndex = 2;


            dataGridSmall.Height = 95;
            dataGridLarge.Height = 95;



        }

        



        private void Select_Item(object sender, EventArgs e)
        {
            updateN();
        }

        private void Select_Warehouse(object sender, EventArgs e)
        {
            updateN();
        }

        private void Select_RunRate(object sender, EventArgs e)
        {
            updateN();
        }

        private void radioQT_CheckedChanged(object sender, EventArgs e)
        {
            if (radioQT.Checked)
            {
                enableTimeBasedOrderQty();
            }
            else
            {
                disableTimeBasedOrderQty();
            }
        }



        private void radioQGeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (radioQGeneral.Checked)
            {
                disableTimeBasedOrderQty();
                refreshSelectedRow(-1);
            }
            else
            {
                enableTimeBasedOrderQty();
            }
        }



        private void dataGridSmall_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboItem.SelectedIndex = 0;
            comboWH.SelectedIndex = e.ColumnIndex;
            comboRR.SelectedIndex = e.RowIndex;
        }

        private void dataGridLarge_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboItem.SelectedIndex = 1;
            comboWH.SelectedIndex = e.ColumnIndex;
            comboRR.SelectedIndex = e.RowIndex;
        }

        private void dataGridSmall_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            updateN();
        }

        private void dataGridLarge_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            updateN();
        }


        private void btnOpen_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\Document\\Solutions\\Aging and Out of Stock in Stores\\Data in ERP";
            openFileDialog1.Filter = "xls(*.xls;*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                itemCode.Clear();

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open("C:\\Document\\Solutions\\Aging and Out of Stock in Stores\\Data in ERP\\ItemCode.xlsx", 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                range = xlWorkSheet.UsedRange;
                for (int i = 2; i <= range.Rows.Count; i++)
                {
                    ItemCode tmpItemCode = new ItemCode();
                    tmpItemCode.itemcode = (range.Cells[i, 1] as Excel.Range).Value2;
                    tmpItemCode.description = (range.Cells[i, 3] as Excel.Range).Value2;
                    tmpItemCode.length = (range.Cells[i, 5] as Excel.Range).Value2;
                    tmpItemCode.width = (range.Cells[i, 6] as Excel.Range).Value2;
                    tmpItemCode.height = (range.Cells[i, 7] as Excel.Range).Value2;
                    tmpItemCode.age = (range.Cells[i, 11] as Excel.Range).Value2;
                    tmpItemCode.level = (range.Cells[i, 12] as Excel.Range).Value2;
                    itemCode.Add(tmpItemCode);
                }
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                string[] multiFile = openFileDialog1.FileNames;
                for (int idxFile = 0; idxFile < multiFile.Length; idxFile++)
                {
                    config.model.Clear();
                    realInventory.Clear();
                    cmpRealInventory.Clear();
                    cmpSimuInventory.Clear();
                    config.POInfo.Clear();
                    dataGridResult.Rows.Clear();
                    realPOETA.clearRealPO();

                    string selectedFileName = openFileDialog1.FileNames[idxFile].ToString();

                    char[] delimiters = { '-', '.' };
                    string[] whName = selectedFileName.Split(delimiters);

                    string whSize = getWHSize(whName[1]);

                    if (whSize == "B") { comboWH.SelectedIndex = 2; } else if (whSize == "M") { comboWH.SelectedIndex = 1; } else { comboWH.SelectedIndex = 0; }

                    char delimiter = '\\';
                    string[] itemName = whName[0].Split(delimiter);

                    txtItem.Text = itemName[itemName.Length - 1];
                    for(int i = 0; i < itemCode.Count; i++)
                    {
                        if (itemCode[i].itemcode.Contains(txtItem.Text))
                        {
                            txtItemDesciption.Text = itemCode[i].description;
                            txtItemLevel.Text = itemCode[i].level;
                            txtAge.Text = itemCode[i].age.ToString();
                            switch (itemCode[i].level)
                            {
                                case "T6":
                                case "T5":
                                    comboRR.SelectedIndex = 2;
                                    break;
                                case "T4":
                                case "T3":
                                    comboRR.SelectedIndex = 1;
                                    break;
                                default:
                                    comboRR.SelectedIndex = 0;
                                    break;
                            }
                            break;
                        }
                    }

                    txtWH.Text = whName[1];
                    if (whSize == "B")
                    {
                        txtWHSize.Text = "Big";
                    }
                    else if (whSize == "M")
                    {
                        txtWHSize.Text = "Medium";
                    }
                    else
                    {
                        txtWHSize.Text = "Small";
                    }




                    int rw = 0;
                    int cl = 0;
                    try
                    {
                        xlApp = new Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(selectedFileName, 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                        range = xlWorkSheet.UsedRange;
                        rw = range.Rows.Count;
                        cl = range.Columns.Count;

                        if (chkWriteExcel.Checked)
                        {
                            xlApp.DisplayAlerts = false;
                            for (int i = xlWorkBook.Worksheets.Count; i > 1; i--)
                            {
                                xlWorkBook.Worksheets[i].Delete();
                            }
                            xlApp.DisplayAlerts = true;
                        }




                        for (int i = rw; i > 1; i--)
                        {
                            generateRealInventory(i);
                        }

                        generateCmpRealInventory();

                        generateModels();

                        generateSimuInventory();



                        xlApp.DisplayAlerts = false;
                        xlWorkBook.SaveAs(selectedFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
            false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                        xlWorkBook.Close(true, null, null);
                        xlApp.Quit();

                        Marshal.ReleaseComObject(xlWorkSheet);
                        Marshal.ReleaseComObject(xlWorkBook);
                        Marshal.ReleaseComObject(xlApp);
                    }
                    finally
                    {

                    }
                    
                }
                MessageBox.Show("Convertion is Done.");
            }
        }
    
        
    
        private void generateRealInventory(int i)
        {
            InventoryHistory tmpInventory = new InventoryHistory();
            
                int rw = range.Rows.Count;

                //get Log Date
                tmpInventory.Date = (range.Cells[i, 1] as Excel.Range).Value2;
            if (String.IsNullOrEmpty(tmpInventory.Date)) { return; }

            //get Operation Code
            tmpInventory.Operation = (range.Cells[i, 2] as Excel.Range).Value2;

            //POP
            string tmpOperation = tmpInventory.Operation.Substring(0, 2);
            if (tmpInventory.Operation.Contains("POP"))
            {
                removeOP(tmpInventory.Operation, 1);
                opPOP = tmpInventory.Operation;
                return;
            }

            //PO
            if (tmpOperation == "PO")
            {

                //if different from next, continue 
                if (i > 2)
                {
                    string nextOP = (range.Cells[i - 1, 2] as Excel.Range).Value2;
                    if (nextOP.Equals(tmpInventory.Operation)) { return; }
                }

                //if wrong operation, continue
                if (Convert.ToInt32((range.Cells[i, 6] as Excel.Range).Value2) <= 0) { return; }

                //remove (invalid) same PO placed before
                removeOP(tmpInventory.Operation);

                //valid PO, add to list
                tmpInventory.opPOP = opPOP;
                tmpInventory.OH = Convert.ToInt32((range.Cells[i, 4] as Excel.Range).Value2);
                tmpInventory.SA = Convert.ToInt32((range.Cells[i, 9] as Excel.Range).Value2);
                if (i < rw)
                {
                    tmpInventory.PO = Convert.ToInt32((range.Cells[i, 6] as Excel.Range).Value2)
                        - Convert.ToInt32((range.Cells[i + 1, 6] as Excel.Range).Value2);
                }
                else
                {
                    tmpInventory.PO = Convert.ToInt32((range.Cells[i, 6] as Excel.Range).Value2);
                }
                realInventory.Add(tmpInventory);
                opPOP = "";
                    return;
            }

            //stock transfer
            if (tmpOperation == "ST" || tmpInventory.Operation.Contains("Fix Data Init"))
            {
                //if same as next, continue 
                if (i > 2)
                {
                    string nextOP = (range.Cells[i - 1, 2] as Excel.Range).Value2;
                    if (nextOP.Equals(tmpInventory.Operation)) { return; }
                }

                //if SA is unchanged , continue
                tmpInventory.SA = Convert.ToInt32((range.Cells[i, 9] as Excel.Range).Value2);
                if (i < rw)
                {
                    int lastSA = Convert.ToInt32((range.Cells[i + 1, 9] as Excel.Range).Value2);
                    if (tmpInventory.SA == lastSA) { return; }
                }
                //valid ITI or ITO
                tmpInventory.OH = Convert.ToInt32((range.Cells[i, 4] as Excel.Range).Value2);
                realInventory.Add(tmpInventory);
                    return;
            }

            //ship out of store
            if (tmpOperation == "SP")
            {
                tmpInventory.OH = Convert.ToInt32((range.Cells[i, 4] as Excel.Range).Value2);
                tmpInventory.SA = Convert.ToInt32((range.Cells[i, 9] as Excel.Range).Value2);
                if (i < rw)
                {//index in realInventory is less 1 than range 
                    tmpInventory.Sold = realInventory[realInventory.Count - 1].SA - tmpInventory.SA;
                }
                else
                {
                    tmpInventory.Sold = 1;
                }
                realInventory.Add(tmpInventory);
                    return;
            }

            //online order, pickup in store
            if (tmpOperation == "SO")
            {
                tmpInventory.OH = Convert.ToInt32((range.Cells[i, 4] as Excel.Range).Value2);
                tmpInventory.SA = Convert.ToInt32((range.Cells[i, 9] as Excel.Range).Value2);
                tmpInventory.PO = Convert.ToInt32((range.Cells[i, 6] as Excel.Range).Value2);
                if (i < rw)
                {//index in realInventory is less 1 than range 
                    tmpInventory.Sold = realInventory[realInventory.Count - 1].SA - tmpInventory.SA;
                }
                else
                {
                    tmpInventory.Sold = 1;
                }
                realInventory.Add(tmpInventory);
                    return;
            }
        }

        private void removeOP(string curOP, int option = 0)
        {
            int indext = 0;
            int indexr = 0;
            List<InventoryHistory> tmpInv = new List<InventoryHistory>(realInventory);
            //remove (invalid) related PO placed before
            if(option == 0)
            {
                foreach (InventoryHistory tmp in tmpInv)
                {
                    if (tmp.Operation.Equals(curOP))
                    {
                        realInventory.RemoveAt(indexr);
                        indexr--;
                    }
                    indext++;
                    indexr++;
                }
            } else
            {
                foreach (InventoryHistory tmp in tmpInv)
                {
                    if (tmp.opPOP.Equals(curOP))
                    {
                        realInventory.RemoveAt(indexr);
                        indexr--;
                    }
                    indext++;
                    indexr++;
                }
            }
        }

        private void generateCmpRealInventory()
        {
            
            int index = 0;


            

            
            foreach (InventoryHistory inventory in realInventory)
            {
                cmpInventory tmpInventory = new cmpInventory();
                DateTime curDate = Convert.ToDateTime(Convert.ToDateTime(inventory.Date).ToString("MM/dd/yyyy"));
                
                
                if (index == 0)
                {
                    tmpInventory.Date = curDate.ToString("MM/dd/yyyy");
                    tmpInventory.OH = inventory.SA;
                    tmpInventory.Q = inventory.PO;
                    tmpInventory.Sold = inventory.Sold;
                    cmpRealInventory.Add(tmpInventory);

                    index++;
                }
                else
                {
                    //operations happened on same day
                    DateTime lastDate = Convert.ToDateTime(cmpRealInventory[cmpRealInventory.Count - 1].Date);
                    if (lastDate == curDate)
                    {
                        if (inventory.Operation.Contains("SP") || inventory.Operation.Contains("SO"))
                        {
                            cmpRealInventory[cmpRealInventory.Count - 1].Sold += inventory.Sold;
                            cmpRealInventory[cmpRealInventory.Count - 1].OH = inventory.SA;
                        }
                        else if (inventory.Operation.Contains("ST"))
                        {
                            cmpRealInventory[cmpRealInventory.Count - 1].OH = inventory.SA;
                            if (realPOETA.realPO.Count > 0)
                            {
                                int eta = (int)(curDate - Convert.ToDateTime(realPOETA.realPO[0].Item1)).TotalDays;
                                int day = Convert.ToInt32(Convert.ToDateTime(realPOETA.realPO[0].Item1).DayOfWeek);

                                realPOETA.etaDay[day].eta += eta;
                                realPOETA.etaDay[day].num += 1;

                                realPOETA.realPO.RemoveAt(0);
                            }
                        } 
                        else if (inventory.Operation.Contains("PO"))
                        {
                            cmpRealInventory[cmpRealInventory.Count - 1].Q += inventory.PO;
                            Tuple<string> tmpPO = new Tuple<string>(cmpRealInventory[cmpRealInventory.Count - 1].Date);
                            realPOETA.realPO.Add(tmpPO);

                        }
                    }
                    else
                    {
                        DateTime dt = new DateTime();
                        dt = lastDate.AddDays(1);
                        while(dt < curDate)
                        {
                            cmpInventory newInventory = new cmpInventory();
                            newInventory.Date = dt.ToString("MM/dd/yyyy");
                            newInventory.OH = cmpRealInventory[cmpRealInventory.Count - 1].OH;
                            cmpRealInventory.Add(newInventory);
                            dt = dt.AddDays(1);
                        }
                        tmpInventory.Date = curDate.Date.ToString("MM/dd/yyyy");
                        tmpInventory.OH = inventory.SA;
                        tmpInventory.Q = inventory.PO;
                        tmpInventory.Sold = inventory.Sold;
                        cmpRealInventory.Add(tmpInventory);

                        if (inventory.Operation.Contains("ST"))
                        {
                            if (realPOETA.realPO.Count > 0)
                            {
                                int eta = (int)(curDate - Convert.ToDateTime(realPOETA.realPO[0].Item1)).TotalDays;
                                int day = Convert.ToInt32(Convert.ToDateTime(realPOETA.realPO[0].Item1).DayOfWeek);

                                realPOETA.etaDay[day].eta += eta;
                                realPOETA.etaDay[day].num += 1;
                                
                                realPOETA.realPO.RemoveAt(0);
                            }
                        }
                        else if (inventory.Operation.Contains("PO"))
                        {
                            Tuple<string> tmpPO = new Tuple<string>(tmpInventory.Date);
                            realPOETA.realPO.Add(tmpPO);
                        }
                    }
                }
                
            }

            Model tmpModel = new Model();
            int sold = 0;
            int unitsPerDay = 0;
            int daysOutstock = 0;
            int orderTimes = 0;

            foreach (cmpInventory inventory in cmpRealInventory)
            {
                if (inventory.Sold > 0)
                {
                    sold += inventory.Sold;
                }
                if (inventory.OH == 0)
                {
                    daysOutstock++;
                }
                if (inventory.Q > 0)
                {
                    orderTimes++;
                }
                unitsPerDay += inventory.OH;
            }
            unitsPerDay = unitsPerDay / (cmpRealInventory.Count - 1);
            tmpModel.Result = new Tuple<string, int, int, int, int, int>("Real", sold, 0, unitsPerDay, daysOutstock, orderTimes); 
            tmpModel.Rating = Convert.ToDouble(sold - Convert.ToDouble(daysOutstock) * 0.1) / Convert.ToDouble(unitsPerDay);
            config.model.Add(tmpModel);

            int totalNum = 0;
            int totalETA = 0;
            for (int i = 0; i<7; i++)
            {
                totalNum += realPOETA.etaDay[i].num;
                totalETA += realPOETA.etaDay[i].eta;
            }
            if (totalNum == 0) { realPOETA.avgETA = 3; } else { realPOETA.avgETA = totalETA / totalNum; }
            for (int i = 0; i < 7; i++)
            {
                if (realPOETA.etaDay[i].num != 0) { realPOETA.etaDay[i].eta /= realPOETA.etaDay[i].num; }
                else { realPOETA.etaDay[i].eta = realPOETA.avgETA; }
            }

            txtETA.Text = realPOETA.avgETA.ToString();
            

            string s = string.Format("{0:N2}", config.model[0].Rating);
            dataGridResult.Rows.Add(config.model[0].Result.Item1,
                s,
                config.model[0].Result.Item2,
                config.model[0].Result.Item3,
                config.model[0].Result.Item4,
                config.model[0].Result.Item5,
                config.model[0].Result.Item6);

            if (chkWriteExcel.Checked)
            {
                Excel.Worksheet newWorksheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                Excel.Range range;
                newWorksheet.Name = "Real";
                range = newWorksheet.UsedRange;
                createRealInventorySheet(range);
            }
            
        }

        private void generateSimuInventory()
        {
            int idxModel = 0;
            

            foreach (Model model in config.model)
            {
                cmpSimuInventory.Clear();
                config.POInfo.Clear();
                
                if (idxModel == 0) { idxModel++; continue; }
                int index = 0;
                
                foreach (cmpInventory inventory in cmpRealInventory)
                {
                    checkWarehouseInventory(index,idxModel);
                    index++;
                }
                int sold = 0;
                int lose = 0;
                int unitsPerDay = 0;
                int daysOutstock = 0;
                int orderTimes = 0;
                Model tmpModel = new Model();
                foreach (cmpInventory inventory in cmpSimuInventory)
                {
                    if (inventory.Sold > 0)
                    {
                        sold += inventory.Sold;
                    }
                    if (inventory.Lose < 0)
                    {
                        lose += inventory.Lose;
                    }
                    if (inventory.OHnew == 0)
                    {
                        daysOutstock++;
                    }
                    if (inventory.Q > 0)
                    {
                        orderTimes++;
                    }
                    unitsPerDay += inventory.OHnew;
                }
                unitsPerDay = unitsPerDay / (cmpRealInventory.Count - 1);
                config.model[idxModel].Result = new Tuple<string, int, int, int, int, int>(config.model[idxModel].Result.Item1, sold, lose, unitsPerDay, daysOutstock, orderTimes);
                config.model[idxModel].Rating = Convert.ToDouble(sold+lose*2-Convert.ToDouble(daysOutstock)*0.1) / Convert.ToDouble(unitsPerDay);

                string s = string.Format("{0:N2}", config.model[idxModel].Rating);

                dataGridResult.Rows.Add(config.model[idxModel].Result.Item1,
                    s,
                    config.model[idxModel].Result.Item2,
                    config.model[idxModel].Result.Item3,
                    config.model[idxModel].Result.Item4,
                    config.model[idxModel].Result.Item5,
                    config.model[idxModel].Result.Item6);
                if (chkWriteExcel.Checked)
                {
                    Worksheet newWorksheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                   
                    if (chkOptimizeQ.Checked)
                    {
                        newWorksheet.Name = config.model[idxModel].Result.Item1 + "Q";
                    }
                    else
                    {
                        newWorksheet.Name = config.model[idxModel].Result.Item1;
                    }
                    
                    createSimuInventorySheet(newWorksheet, idxModel);

                }
                idxModel++;
            }

            if (chkWriteExcel.Checked)
            {
                Excel.Worksheet newWorksheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[1]);
                newWorksheet.Name = "Result";
                createResultSheet(newWorksheet);
            }

        }
        

        private void generateModels()
        {
           for(int i = 0; i<3; i++)
            {//Run Rate= 0:L 1:M 2:H
                Model tmpModel = new Model();
                string modelName = setModelName(i,"G");
                tmpModel.Method = 1;
                if (comboItem.SelectedIndex == 0)
                {
                    tmpModel.DftN = Convert.ToInt32(dataGridSmall.Rows[i].Cells[comboWH.SelectedIndex].Value);
                }
                else
                {
                    tmpModel.DftN = Convert.ToInt32(dataGridLarge.Rows[i].Cells[comboWH.SelectedIndex].Value);
                }
                config.model.Add(tmpModel);
                config.model[config.model.Count-1].Result = new Tuple<string, int, int, int, int, int>(modelName, 0, 0, 0, 0, 0);

            }


            for (int i = 0; i<3; i++)
            {//Run Rate= 0:L 1:M 2:H
                Model tmpModel = new Model();
                string prefixName = "TS(";
                if (chkResetT.Checked) { prefixName += "0"; } else { prefixName += "1"; }
                if (chkResetS.Checked) { prefixName += "0)"; } else { prefixName += "1)"; }

                string modelName = setModelName(i, prefixName);
                tmpModel.Method = 2;
                if (comboItem.SelectedIndex == 0)
                {
                    tmpModel.DftN = Convert.ToInt32(dataGridSmall.Rows[i].Cells[comboWH.SelectedIndex].Value);
                }
                else
                {
                    tmpModel.DftN = Convert.ToInt32(dataGridLarge.Rows[i].Cells[comboWH.SelectedIndex].Value);
                }
                config.model.Add(tmpModel);
                config.model[config.model.Count - 1].Result = new Tuple<string, int, int, int, int, int>(modelName, 0, 0, 0, 0, 0);
                simuSetQuantityTimeBased();
            }
        }

        public string setModelName(int i, string method)
        {
            string modelName;
            string hightLight = "";
            string item = "I(S&M)";
            string warehouse = "W(S)";
            string rr = "RR(L)";
            if (comboItem.SelectedIndex == 1) { item = "I(L)"; }
            if (comboWH.SelectedIndex == 1) { warehouse = "W(M)"; } else if (comboWH.SelectedIndex == 2) { warehouse = "W(B)"; }
            if (comboRR.SelectedIndex == i)
            {
                hightLight = "#";
            }
            if (i == 1) { rr = "RR(M)"; } else if (i == 2) { rr = "RR(H)"; }
            modelName = hightLight + method+ "-" + item + "-" + warehouse + "-" + rr;
            
            return modelName;
        }

        private void checkWarehouseInventory(int index, int idxModel)
        {
            loadItem(index, idxModel);
            // check if products are coming today
            if (config.POInfo.Count > 0)
            {
                foreach(Tuple<string,int> poInfor in config.POInfo)
                {
                    if(poInfor.Item1 == cmpSimuInventory[index].Date)
                    {
                        simuUpdateStockReceiving(index, poInfor.Item2);
                    }
                }
                config.POInfo.RemoveAll(x => x.Item1 == cmpSimuInventory[index].Date);
            }

            // check if products are sold today
            if (cmpRealInventory[index].Sold > 0)
            {
                simuUpdateSold(index, idxModel);
                
            }


        }

        private void loadItem(int index, int idxModel)
        {
            cmpInventory curInventory = new cmpInventory();
            if (index == 0)
            {
                curInventory.OH = curInventory.OHnew = config.model[idxModel].DftN;
            }
            else
            {
                curInventory.OH = cmpSimuInventory[index - 1].OH;
                curInventory.OHnew = cmpSimuInventory[index - 1].OHnew;
                curInventory.T = cmpSimuInventory[index - 1].T + 1;
                curInventory.S = cmpSimuInventory[index - 1].S;
            }
            curInventory.Date = cmpRealInventory[index].Date;
            cmpSimuInventory.Add(curInventory);
        }

        private void simuUpdateStockReceiving(int index, int Receiving)
        {
            if (chkResetS.Checked) { cmpSimuInventory[index].S = 0; }
            if (chkResetT.Checked) { cmpSimuInventory[index].T = 0; }
            cmpSimuInventory[index].OH = cmpSimuInventory[index].OHnew = cmpSimuInventory[index].OHnew + Receiving;
        }

        private void simuUpdateSold(int index, int idxModel)
        {
            bool allowPO = false;
            if (config.POInfo.Count == 0 || (chkMultiOrder.Checked && cmpSimuInventory[index].OHnew > 0)) { allowPO = true; }
            
            
            if (cmpSimuInventory[index].OHnew < cmpRealInventory[index].Sold)
            {
                cmpSimuInventory[index].Lose = cmpSimuInventory[index].OHnew - cmpRealInventory[index].Sold;
                cmpSimuInventory[index].Sold = cmpSimuInventory[index].OHnew;
                cmpSimuInventory[index].S += cmpSimuInventory[index].Sold;
                cmpSimuInventory[index].OHnew = 0;
            }
            else
            {
                cmpSimuInventory[index].Sold = cmpRealInventory[index].Sold;
                cmpSimuInventory[index].S += cmpSimuInventory[index].Sold;
                cmpSimuInventory[index].OHnew -= cmpSimuInventory[index].Sold;
            }
            // check if PO criterion are met
            if ((cmpSimuInventory[index].S >= (cmpSimuInventory[index].OH + 1) / 2 || cmpSimuInventory[index].OHnew == 0) && allowPO)
            {
                simuPlacePO(index, idxModel);
            }
        }

        private void simuPlacePO(int index, int idxModel)
        {
            if (config.POInfo.Count > 0)
            {
                cmpSimuInventory[index].Q = config.POInfo[config.POInfo.Count-1].Item2 / 2;
            }
            else if (config.model[idxModel].Method == 2)
            {
                simuGetQtyTimeBased(index, idxModel);
            }
            else if(config.model[idxModel].Method == 1)
            {
                if (cmpSimuInventory[index].S >= (cmpSimuInventory[index].OH + 1) / 2)
                {
                    cmpSimuInventory[index].Q = config.model[idxModel].DftN - cmpSimuInventory[index].OHnew; 
                }
                else
                {
                    cmpSimuInventory[index].Q = cmpSimuInventory[index].S;
                }
            }

            if (config.model[idxModel].numPO == 0)
            {
                config.model[idxModel].numPO = 1;
                config.model[idxModel].totalQ = cmpSimuInventory[index].Q;
            }
            if (chkOptimizeQ.Checked)
            { 
                DateTime clearancePoint = Convert.ToDateTime(cmpSimuInventory[0].Date).AddDays(Convert.ToInt32(txtClearancePoint.Text));
                double avgQ = config.model[idxModel].totalQ / config.model[idxModel].numPO;

                if (Convert.ToDateTime(cmpSimuInventory[index].Date) >= clearancePoint)
                {// in clearance period, reduce PO Qty
                    if (cmpSimuInventory[index].Q >= avgQ * 1.5)
                    {// clearance 
                        cmpSimuInventory[index].Q = Convert.ToInt32(avgQ / 2);
                    }
                    else
                    {// normal sales
                        cmpSimuInventory[index].Q = cmpSimuInventory[index].Q / 2;
                    }
                }
                else
                {// in normal sales period
                    if (cmpSimuInventory[index].Q >= avgQ * 1.5)
                    {// large promotion 
                        cmpSimuInventory[index].Q = Convert.ToInt32(avgQ);
                    }
                }
            }
            if (cmpSimuInventory[index].Q <= 1) { cmpSimuInventory[index].Q = 2; }
            
            config.model[idxModel].totalQ += cmpSimuInventory[index].Q;
            config.model[idxModel].numPO++;
            
            cmpSimuInventory[index].T = 0;
            cmpSimuInventory[index].S = 0;

            simuCalculateETA(index);

            Tuple<string, int> poInfo = new Tuple<string, int>(cmpSimuInventory[index].ETA, cmpSimuInventory[index].Q);
            config.POInfo.Add(poInfo);
        }

        private void simuSetQuantityTimeBased()
        {
            int n;
            
            for (int i = 0; i < dataGridAdjustQ.RowCount; i++)
            {
                TimeBaseRule timeBase = new TimeBaseRule();
                for (int j = 0; j < dataGridAdjustQ.ColumnCount; j++)
                {
                    if (int.TryParse(dataGridAdjustQ.Rows[i].Cells[j].Value.ToString(), out n))
                    {
                        switch (j)
                        {
                            case 0: //T>
                                timeBase.T1 = n;
                                break;
                            case 1: //S>
                                timeBase.T2 = n;
                                break;
                            case 2: //T<=
                                timeBase.S1 = n;
                                break;
                            case 3: //S<
                                timeBase.S2 = n;
                                break;
                            case 4: //*S
                                timeBase.MD = n;
                                break;
                            case 5: //+S
                                timeBase.AD = n;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (j)
                        {
                            case 0: //T>
                                timeBase.T1 = -1;
                                break;
                            case 1: //T<=
                                timeBase.T2 = 1000;
                                break;
                            case 2: //S>
                                timeBase.S1 = 0;
                                break;
                            case 3: //S<
                                timeBase.S2 = 1000;
                                break;
                            case 4: //*S
                                timeBase.MD = 1;
                                break;
                            case 5: //+S
                                timeBase.AD = 0;
                                break;
                            default:
                                break;
                        }
                    }
                }
                config.model[config.model.Count - 1].TimeBase.Add(timeBase);
            }
            
        }

        private void simuGetQtyTimeBased(int indexInventory, int indexModel)
        {
            List<TimeBaseRule> timeBase = new List<TimeBaseRule>(config.model[indexModel].TimeBase);
            
            for (int i = 0; i < timeBase.Count; i++)
            {
                if (cmpSimuInventory[indexInventory].T > timeBase[i].T1 && cmpSimuInventory[indexInventory].T <= timeBase[i].T2)
                {
                    if (cmpSimuInventory[indexInventory].S > timeBase[i].S1 && cmpSimuInventory[indexInventory].S <= timeBase[i].S2)
                    {
                        if (timeBase[i].MD >= 0)
                        {
                            cmpSimuInventory[indexInventory].Q = cmpSimuInventory[indexInventory].S * timeBase[i].MD + timeBase[i].AD;
                        }
                        else
                        {
                            cmpSimuInventory[indexInventory].Q = cmpSimuInventory[indexInventory].S / Math.Abs(timeBase[i].MD) + timeBase[i].AD;
                        }
                        break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridResult.Rows.Clear();
        }

        private void simuCalculateETA(int index)
        {
            DateTime curDate = Convert.ToDateTime(cmpSimuInventory[index].Date);
            int day = Convert.ToInt32(curDate.DayOfWeek);
            int eta = realPOETA.etaDay[day].eta;
            if(eta == 0) { eta = realPOETA.avgETA; }

            cmpSimuInventory[index].ETA = Convert.ToDateTime(cmpSimuInventory[index].Date).AddDays(eta).ToString("MM/dd/yyyy");
        }

        private void createRealInventorySheet(Excel.Range range)
        {
            int rowResult = 1;
            int rowDetail = rowResult + 3;

            range.Cells[rowResult, 1] = "Sold";
            range.Cells[rowResult, 2] = "Units/Day";
            range.Cells[rowResult, 3] = "Days of OutStock";
            range.Cells[rowResult, 4] = "Order Times";
            range.Cells[rowResult, 5] = "ETA(Mon)";
            range.Cells[rowResult, 6] = "ETA(Tue)";
            range.Cells[rowResult, 7] = "ETA(Wed)";
            range.Cells[rowResult, 8] = "ETA(Thu)";
            range.Cells[rowResult, 9] = "ETA(Fri)";
            range.Cells[rowResult, 10] = "ETA(Sat)";
            range.Cells[rowResult, 11] = "ETA(Sun)";
            range.Cells[rowResult, 12] = "ETA(Avg)";

            range.Cells[rowResult + 1, 1] = config.model[0].Result.Item2;
            range.Cells[rowResult + 1, 2] = config.model[0].Result.Item4;
            range.Cells[rowResult + 1, 3] = config.model[0].Result.Item5;
            range.Cells[rowResult + 1, 4] = config.model[0].Result.Item6;
            for(int i = 0; i<7; i++)
            {
                range.Cells[rowResult + 1, 5 + i] = realPOETA.etaDay[i].eta;
            }

            range.Cells[rowResult + 1, 12] = realPOETA.avgETA;
            
            range.Cells[rowDetail, 1] = "Date";
            range.Cells[rowDetail, 2] = "Operation";
            range.Cells[rowDetail, 3] = "Sold";
            range.Cells[rowDetail, 4] = "OH";
            range.Cells[rowDetail, 5] = "SA";
            range.Cells[rowDetail, 6] = "PO";
            range.Cells[rowDetail, 7] = "POP";

            range.Cells[rowDetail, 9] = "Date";
            range.Cells[rowDetail, 10] = "Sold";
            range.Cells[rowDetail, 11] = "OH";
            range.Cells[rowDetail, 12] = "PO";


            int rwindex = rowDetail + 1;
            foreach (InventoryHistory realInventory in realInventory)
            {
                range.Cells[rwindex, 1] = realInventory.Date;
                range.Cells[rwindex, 2] = realInventory.Operation;
                range.Cells[rwindex, 3] = realInventory.Sold;
                range.Cells[rwindex, 4] = realInventory.OH;
                range.Cells[rwindex, 5] = realInventory.SA;
                range.Cells[rwindex, 6] = realInventory.PO;
                range.Cells[rwindex, 7] = realInventory.opPOP;
                rwindex++;
            }

            rwindex = rowDetail + 1;
            foreach (cmpInventory cmpInventory in cmpRealInventory)
            {

                range.Cells[rwindex, 9] = cmpInventory.Date;
                range.Cells[rwindex, 10] = cmpInventory.Sold;
                range.Cells[rwindex, 11] = cmpInventory.OH;
                range.Cells[rwindex, 12] = cmpInventory.Q;

                rwindex++;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            config.model.Clear();
            config.POInfo.Clear();
            Model tmpModel = new Model();
            config.model.Add(tmpModel);
            generateModels();
            generateSimuInventory();
        }


        private void createSimuInventorySheet(Worksheet newWorksheet, int idxModel)
        {
            range = newWorksheet.UsedRange;
            int rowResult = 1;
            int rowDetail = rowResult + 3;

            range.Cells[rowResult, 1] = "Sold";
            range.Cells[rowResult, 2] = "Lose";
            range.Cells[rowResult, 3] = "Units/Day";
            range.Cells[rowResult, 4] = "Days of OutStock";
            range.Cells[rowResult, 5] = "Order Times";
            range.Cells[rowResult + 1, 1] = config.model[idxModel].Result.Item2;
            range.Cells[rowResult + 1, 2] = config.model[idxModel].Result.Item3;
            range.Cells[rowResult + 1, 3] = config.model[idxModel].Result.Item4;
            range.Cells[rowResult + 1, 4] = config.model[idxModel].Result.Item5;
            range.Cells[rowResult + 1, 5] = config.model[idxModel].Result.Item6;

            

            range.Cells[rowDetail, 1] = "Date";
            range.Cells[rowDetail, 2] = "Sold";
            range.Cells[rowDetail, 5] = "Lose";
            range.Cells[rowDetail, 3] = "OH";
            range.Cells[rowDetail, 4] = "PO";

            int rwindex = rowDetail + 1;
            foreach (cmpInventory cmpInventory in cmpSimuInventory)
            {

                range.Cells[rwindex, 1] = cmpInventory.Date;
                range.Cells[rwindex, 2] = cmpInventory.Sold;
                range.Cells[rwindex, 3] = cmpInventory.OHnew; 
                range.Cells[rwindex, 4] = cmpInventory.Q;
                range.Cells[rwindex, 5] = cmpInventory.Lose;

                rwindex++;
            }
            
        }

        

        private void createResultSheet(Worksheet newWorksheet)
        {
            range = newWorksheet.UsedRange;
            range.Cells[2, 2] = "Item";
            range.Cells[2, 3] = txtItem.Text;
            range.Cells[2, 5] = "Level";
            range.Cells[2, 6] = txtItemLevel.Text;
            range.Cells[2, 8] = "Age";
            range.Cells[2, 9] = txtAge.Text;
            

            range.Cells[3, 3] = txtItemDesciption.Text;
            range.Cells[5, 2] = "WH";
            range.Cells[5, 3] = txtWH.Text;
            range.Cells[5, 5] = "Size";
            range.Cells[5, 6] = txtWHSize.Text;

            range.Cells[7, 1] = txtWH.Text;
            range.Cells[7, 2] = "Rating";
            range.Cells[7, 3] = "Sold";
            range.Cells[7, 4] = "Lose";
            range.Cells[7, 5] = "Units/Day";
            range.Cells[7, 6] = "Days of OutStock";
            range.Cells[7, 7] = "Number of POs";

            for(int i = 0; i<dataGridResult.RowCount; i++)
            {
                for(int j = 0; j < dataGridResult.ColumnCount; j++)
                {
                    if(dataGridResult.Rows[i].Cells[j].Value == null) { continue; }
                    range.Cells[i + 8, j+1] = dataGridResult.Rows[i].Cells[j].Value.ToString();
                }
            }

            int bottom = 400;
            
            var charts = newWorksheet.ChartObjects() as ChartObjects;
            var chartObject = charts.Add(10, 200, 1100, 200) as ChartObject;
            var chart = chartObject.Chart;
            Excel.Worksheet tmpWorkSheet = xlWorkBook.Worksheets.Item[3];
            Excel.Range tmpRange = tmpWorkSheet.UsedRange;

            var chartRange = xlWorkBook.Worksheets[3].Range["I4", "L"+ tmpRange.Rows.Count];
            chart.SetSourceData(chartRange);

            chart.ChartType = XlChartType.xlColumnClustered;
            
            string tmpName = tmpWorkSheet.Name + "   R(" + (range.Cells[8, 2] as Excel.Range).Value2 +
                ") S(" + (range.Cells[8, 3] as Excel.Range).Value2 +
                ") L(" + (range.Cells[8, 4] as Excel.Range).Value2 +
                ") U(" + (range.Cells[8, 5] as Excel.Range).Value2 +
                ") OS(" + (range.Cells[8, 6] as Excel.Range).Value2 +
                ") P(" + (range.Cells[8, 7] as Excel.Range).Value2 + ")";
            chart.ChartWizard(Source: chartRange, Title: tmpName);

            for (int i = 4; i < 10; i++)
            {
                var chartObject1 = charts.Add(10, bottom + 10, 1100, 200) as ChartObject;
                var chart1 = chartObject1.Chart;
                Excel.Worksheet tmpWorkSheet1 = xlWorkBook.Worksheets.Item[i];
                Excel.Range tmpRange1 = tmpWorkSheet.UsedRange;

                var chartRange1 = xlWorkBook.Worksheets[i].Range["A4", "E" + tmpRange1.Rows.Count];
                chart1.SetSourceData(chartRange1);

                chart1.ChartType = XlChartType.xlColumnClustered;
                tmpName = tmpWorkSheet1.Name + "   R(" + (range.Cells[5 + i, 2] as Excel.Range).Value2 +
                ") S(" + (range.Cells[5 + i, 3] as Excel.Range).Value2 +
                ") L(" + (range.Cells[5 + i, 4] as Excel.Range).Value2 +
                ") U(" + (range.Cells[5 + i, 5] as Excel.Range).Value2 +
                ") OS(" + (range.Cells[5 + i, 6] as Excel.Range).Value2 +
                ") P(" + (range.Cells[5 + i, 7] as Excel.Range).Value2 + ")";
                chart1.ChartWizard(Source: chartRange1, Title: tmpName); 
                bottom += 210;
            }
        }

        private void enableTimeBasedOrderQty()
        {
            foreach (DataGridViewRow row in dataGridAdjustQ.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.ForeColor = Color.Black;
                }
            }

            txtQGeneral.ForeColor = Color.Silver;
        }

        private void disableTimeBasedOrderQty()
        {
            foreach (DataGridViewRow row in dataGridAdjustQ.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.ForeColor = Color.Silver;
                }
            }

            txtQGeneral.ForeColor = Color.Black;
        }

        private void refreshSelectedRow(int i)
        {
            foreach (DataGridViewRow row in dataGridAdjustQ.Rows)
            {
                row.Selected = false;
            }
            if (i < 0) { return; }
            dataGridAdjustQ.Rows[i].Selected = true;
        }

        private void updateN()
        {
            if (comboItem.SelectedItem == null || comboWH.SelectedItem == null || comboRR.SelectedItem == null) { return; }

            foreach (DataGridViewRow row in dataGridSmall.Rows)
            {
                row.Selected = false;
            }
            foreach (DataGridViewRow row in dataGridLarge.Rows)
            {
                row.Selected = false;
            }

            if (comboItem.SelectedIndex == 0)
            {
                dataGridSmall.Rows[comboRR.SelectedIndex].Cells[comboWH.SelectedIndex].Selected = true;
            }
            else
            {
                dataGridLarge.Rows[comboRR.SelectedIndex].Cells[comboWH.SelectedIndex].Selected = true;
            }
        }

        public string getWHSize(string whName)
        {
            for (int i = 0; i < WHSize.Length/2; i++)
            {
                if (WHSize[i, 0].Contains(whName))
                {
                    return WHSize[i, 1];
                }
            }
            return "B"; 
        }

        private void loadWHSize()
        {
            WHSize[0, 0] = "Mississauga"; WHSize[0, 1] = "B";
            WHSize[1, 0] = "MU Markham Unionville"; WHSize[1, 1] = "B";
            WHSize[2, 0] = "Brampton"; WHSize[2, 1] = "B";
            WHSize[3, 0] = "Waterloo"; WHSize[3, 1] = "B";
            WHSize[4, 0] = "Etobicoke"; WHSize[4, 1] = "B";
            WHSize[5, 0] = "Toronto Kennedy"; WHSize[5, 1] = "B";
            WHSize[6, 0] = "Ottawa Merivate"; WHSize[6, 1] = "B";
            WHSize[7, 0] = "Vaughan"; WHSize[7, 1] = "B";

            WHSize[8, 0] = "London"; WHSize[8, 1] = "M";
            WHSize[9, 0] = "Toronto Down Town 284"; WHSize[9, 1] = "M";
            WHSize[10, 0] = "Hamilton"; WHSize[10, 1] = "M";
            WHSize[11, 0] = "Laval"; WHSize[11, 1] = "M";
            WHSize[12, 0] = "Greenfield Park"; WHSize[12, 1] = "M";
            WHSize[13, 0] = "Whitby"; WHSize[13, 1] = "M";
            WHSize[14, 0] = "Newmarket"; WHSize[14, 1] = "M";
            WHSize[15, 0] = "Kanata"; WHSize[15, 1] = "M";
            WHSize[16, 0] = "Ottawa Orleans"; WHSize[16, 1] = "M";
            WHSize[17, 0] = "Barrie"; WHSize[17, 1] = "M";
            WHSize[18, 0] = "Berlington"; WHSize[18, 1] = "M";
            WHSize[19, 0] = "Ottawa Downtown"; WHSize[19, 1] = "M";
            WHSize[20, 0] = "Richmond Hill"; WHSize[20, 1] = "M";
            WHSize[21, 0] = "Toronto Mid Town"; WHSize[21, 1] = "M";
            WHSize[22, 0] = "Kingston"; WHSize[22, 1] = "M";
            WHSize[23, 0] = "Ajax"; WHSize[23, 1] = "M";
            WHSize[24, 0] = "Gatineau"; WHSize[24, 1] = "M";

            WHSize[25, 0] = "Milton"; WHSize[25, 1] = "S";
            WHSize[26, 0] = "Montreal"; WHSize[26, 1] = "S";
            WHSize[27, 0] = "West Island"; WHSize[27, 1] = "S";
            WHSize[28, 0] = "St Catharines"; WHSize[28, 1] = "S";
            WHSize[29, 0] = "Oshawa"; WHSize[29, 1] = "S";

            WHSize[30, 0] = "Burnaby"; WHSize[30, 1] = "S";
            WHSize[31, 0] = "Coquitiam"; WHSize[31, 1] = "S";
            WHSize[32, 0] = "Vancouver"; WHSize[32, 1] = "S";
            WHSize[33, 0] = "Toronto Down Town 366"; WHSize[33, 1] = "S";
            WHSize[34, 0] = "Richmond"; WHSize[34, 1] = "S";

        }
    }


    public class InventoryHistory
    {
        public String Date, Operation, opPOP;
        public int Sold;
        public int OH;
        public int SA;
        public int PO;


        public InventoryHistory()
        {
            Date = Operation = opPOP = "";
            OH = SA = PO = 0;
        }
    }

    public class cmpInventory
    {
        public string Date = "" ;
        public int Sold = 0; //sold on this day
        public int OHnew = 0; //Salable, OHnew
        public int Q = 0; //PO Qty
        public int OH; //Threshold of OH
        public int T; //Days since last PO or TO
        public int S; //total of sold
        public int Lose; //supposed to be sold, but failed due to out of stock
        public string ETA; //date of receiving

        public cmpInventory()
        {
            Date = ETA = "";
            Sold = OH = OHnew = Q = T = S = Lose = 0;
        }
    }

}
