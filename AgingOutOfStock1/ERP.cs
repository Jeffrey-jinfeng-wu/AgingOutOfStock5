using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgingOutOfStock1
{
    public partial class Form1 : Form
    {
        protected struct StoreProduct
        {
            public int T; //Days since last purchase/TO/TI
            public int S; //number of sold product since last purchase/TO/TI
            public int N; //top up HO
            public int OH; //threshold of HO to place a PO
            public int OHnew; //salable number in store
            public StoreProduct(int d, int so, int top, int th, int sa)
            {
                T = d;
                S = so;
                N = top;
                OH = th;
                OHnew = sa;
            }
        }
        

        //Whenever products are sold, update Sold number
        private void UpdateSold(StoreProduct oldStProduct, int soldInThisTransaction, out StoreProduct newStProduct)
        {
            newStProduct = oldStProduct;
            newStProduct.S = oldStProduct.S + soldInThisTransaction;
            newStProduct.OHnew -= soldInThisTransaction; 
        }

        //Check at the end of day to see if need to place a PO
        private int checkEndOfDay(StoreProduct oldStProduct, out StoreProduct newStProduct)
        {
            if (oldStProduct.S >= (oldStProduct.OH + 1) / 2 || oldStProduct.OHnew == 0)
            {
                return placePO(oldStProduct, out newStProduct);
            }
            newStProduct = oldStProduct;
            return 0;
        }

        private int placePO(StoreProduct oldStProdcut, out StoreProduct newStProdcut)
        {
            int newPO = 0;
            if (radioQT.Checked)
            {
                getQuantityTimeBased(oldStProdcut, out newPO);

            }
            else if (oldStProdcut.S >= (oldStProdcut.OH + 1) / 2)
            {
                newPO = oldStProdcut.N - oldStProdcut.OHnew;
            }
            else
            {
                newPO = oldStProdcut.S;
            }
            newStProdcut = oldStProdcut;
            newStProdcut.S = 0;
            newStProdcut.T = 0;
            //Create a PO order with numberOfPO
            return newPO;
        }

        private void UpdateStockReceiving(StoreProduct oldStProduct, int receiving, out StoreProduct newStProduct)
        {
            newStProduct = oldStProduct;
            newStProduct.OH = newStProduct.OHnew = oldStProduct.S + receiving;
            newStProduct.S = 0;
        }

        private bool checkIfAllowTO(StoreProduct newStProduct, int TO, out string err)
        {
            if (newStProduct.T < Convert.ToInt32(txtDaysAllowTO.Text))
            {
                err = "T (" + newStProduct.T + ") is less than "+ txtDaysAllowTO.Text+" days.";
                return false;
            }
            else if(TO >= newStProduct.OH - 2 * newStProduct.S)
            {
                err = "TO (" + TO + ") >= OH-2*S = " + newStProduct.OH + "-2*" + newStProduct.S + "=" + (newStProduct.OH - 2 * newStProduct.S).ToString();
                return false;
            }
            else { err = ""; return true; }
        }

        private void updateTO(StoreProduct oldStProduct, int TO, out StoreProduct newStProduct)
        {
            newStProduct = oldStProduct;
            newStProduct.N = oldStProduct.N - TO;
            newStProduct.N = oldStProduct.N - TO;
            newStProduct.OHnew = oldStProduct.OHnew - TO;
            newStProduct.T = 0;
        }

        private void updateTI(StoreProduct oldStProduct, int TI, out StoreProduct newStProduct)
        {
            newStProduct.N = oldStProduct.N + TI;
            newStProduct.OH = oldStProduct.OH + TI;
            newStProduct.OHnew = oldStProduct.OHnew + TI;
            newStProduct.S = oldStProduct.S;
            newStProduct.T = oldStProduct.T;
        }

        private void getQuantityTimeBased(StoreProduct stProduct, out int newPO)
        {
            int i,j,n;
            newPO = 0;
            int[,] tb = new int[dataGridAdjustQ.RowCount, dataGridAdjustQ.ColumnCount]; //store TimeBasedQty configuration

            for (i = 0; i < dataGridAdjustQ.RowCount; i++)
            {
                for (j = 0; j < dataGridAdjustQ.ColumnCount; j++)
                {
                    if (int.TryParse(dataGridAdjustQ.Rows[i].Cells[j].Value.ToString(), out n))
                    {
                        tb[i, j] = n;
                    }
                    else
                    {
                        switch (j)
                        {
                            case 0: //T>
                                tb[i, j] = -1;
                                break;
                            case 2: //S>
                            case 5: //+S
                                tb[i, j] = 0;
                                break;
                            case 1: //T<=
                            case 3: //S<
                                tb[i, j] = 1000;
                                break;
                            case 4: //*S
                                tb[i, j] = 1;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            for (i = 0; i < dataGridAdjustQ.RowCount; i++)
            {
                if (stProduct.T > tb[i, 0] && stProduct.T <= tb[i, 1])
                {
                    if (stProduct.S > tb[i, 2] && stProduct.S <= tb[i, 3])
                    {
                        if (tb[i, 4] >= 0)
                        {
                            newPO = stProduct.S * tb[i, 4] + tb[i, 5];
                            refreshSelectedRow(i); 
                        }
                        else
                        {
                            newPO = stProduct.S / Math.Abs(tb[i, 4]) + tb[i, 5];
                            refreshSelectedRow(i);
                        }
                        break;
                    }
                }
            }
        }
    }
}
