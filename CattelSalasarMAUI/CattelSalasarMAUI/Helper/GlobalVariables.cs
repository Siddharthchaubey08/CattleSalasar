using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Helper
{
    public static class GlobalVariables
    {
        public static int isLoggedin = 0;
        public static int tempProposalId = 0;
        public static int tempPondProposalId = 0;
        public static int tempPondId = 0;
        public static int checkReturnSamepage = 0;
        public static string IMEI_No = "", MobNo = "", vState = "";
        public static Int64 UserId = 0;
        public static Int64 ServerUserId = 0;
        public static string UserName = null;
        public static string Email = null;
        public static System.DateTime ServerDateTime;
        public static bool IsApproved = false;
        public static bool wasScreenOn = true;
        public static bool bShowDataActivity = false;
        public static string QRListRetValId = "";
        public static string QRListRetValName = "";
        public static int DevoteeEventId = 0;
        public static int DevoteeOccasionId = 0;
        public static int DevoteeLocationId = 0;
        public static int DevoteePledgeId = 0;
        public static int DevoteePlaceId = 0;
        public static string DevoteeEventName = "";
        public static string DevoteeOccasionName = "";
        public static string DevoteeLocationName = "";
        public static string DevoteePledgeName = "";
        public static DateTime DevoteePledgeDate = DateTime.Now;

        public static int ScreenHeightDP { get; set; }
        public static int ScreenWidthDP { get; set; }

        public static int ScreenHeightPix { get; set; }
        public static int ScreenWidthPix { get; set; }
        public static float Scale = 0.0f;

        public static int nAdjust = 0;

        public static string strProgress = "";
        public static bool IsProgressRunning = false;
        public static bool IsSynchronizing = false;
        //public static bool ExceptionThrown = false;
        //public static string ExceptionMsg = "";
        public static string Tag = "DPM";
        public static string SyncCount = "0";
        public static DataTable dtVersion = new DataTable();
        //public static ISyncData snc;

        public static int Val(string strval)
        {
            int.TryParse(strval, out int retVal);
            return retVal;
        }

        public static float ValF(string strval)
        {
            float.TryParse(strval, out float retVal);
            return retVal;
        }

        //public static string appUrl = "http://1.22.180.122:9191/"; //RiskCare
        //public static string appUrl = "http://1.22.180.122:8812/";
       // public static string appUrl = "http://adeptinfo.co.in:4329/"; //previouse API
        //public static string appUrl = "http://15.207.209.137:8085/";
       // public static string appUrl = "https://api.salasarservices.in/";    //Clint Live Server(With SSL)
        public static string appUrl = "http://13.200.151.26:8085/";   //Clint Live Server(Without SSL)

        public static string UploadFilePath = "http://182.71.11.91:81/upload/upload1.aspx";
        public static string UploadFilePathBarsana = "http://10.21.14.201/upload/upload1.aspx";
        public static string UploadFilePathVrindavan = "http://10.11.14.201/upload/upload1.aspx";
        //public static string UploadFilePathMangarh = "http://10.41.1.201/upload/upload1.aspx";
        public static string UploadFilePathMangarh = "http://182.71.11.91:81/upload/upload1.aspx";
        public static string UploadFilePathGolokDham = "http://182.71.11.91:81/upload/upload1.aspx"; //"http://182.71.11.91:82/DPMUpload/upload1.aspx";

        public static string FolderPath { get; set; }

        public static string RootDBPath = "";
        public static List<string> ListOfBTPrinters = new List<string>();
        //public static ConcurrentDictionary<string, PrinterDTO> _printers = new ConcurrentDictionary<string, PrinterDTO>();
        public static string strSelectedPrinter = "";

        
        public static bool CheckInternetConnection()
        {
            //string CheckUrl = "http://google.com";
            string CheckUrl = "http://www.google.com";
            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 5000;
                WebResponse iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close();
                return true;
            }
            catch (WebException)
            { return false; }
        }

        public static DataTable dtEnc;

        public static string EncryptText(string str)
        {
            try
            {
                int i; string strLine = "";
                DataView dv = new DataView(dtEnc);
                for (i = 0; i < str.Length; i++)
                {
                    dv.RowFilter = "Plane='" + str[i].ToString().Replace("'", "''") + "'";
                    if (dv.Count > 0)
                    {
                        strLine += dv[0]["Cyfer"] + "";
                    }
                    else
                    {
                        strLine += str.Substring(i, 1);
                    }
                }
                return strLine;
            }
            catch (System.Exception)
            { throw; }
        }

        public static string DecryptText(string str)
        {
            try
            {
                int i; string strLine = "";
                DataView dv = new DataView(dtEnc);
                for (i = 0; i < str.Length; i++)
                {
                    dv.RowFilter = "Cyfer='" + str.Substring(i, 1).Replace("'", "''") + "'";
                    if (dv.Count > 0)
                    {
                        strLine += dv[0]["Plane"];
                    }
                    else
                    {
                        strLine += str.Substring(i, 1);
                    }
                }
                return strLine;
            }
            catch (System.Exception ex)
            { throw ex; }
        }

        public static string initVector = "@1B2c3D4e5F6g7H8";
        public static string hashAlgorithm = "SHA1";
        public static int passwordIterations = 10;
        public static int keySize = 256;
        public static string Encrypt(string plainText, string passPhrase, string saltValue)
        {
            try
            {
                byte[] initVectorBytes;
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);

                byte[] saltValueBytes;
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                byte[] plainTextBytes;
                plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                PasswordDeriveBytes password;
                password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                byte[] keyBytes;
                keyBytes = password.GetBytes((int)(keySize / (double)8));

                RijndaelManaged symmetricKey;
                symmetricKey = new RijndaelManaged
                {
                    Mode = CipherMode.CBC
                };

                ICryptoTransform encryptor;
                encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

                MemoryStream memoryStream;
                memoryStream = new MemoryStream();

                CryptoStream cryptoStream;
                cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes;
                cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                string cipherText;
                cipherText = Convert.ToBase64String(cipherTextBytes);

                return cipherText;
            }
            catch (System.Exception ex)
            { throw ex; }
        }

        public static string Decrypt(string cipherText, string passPhrase, string saltValue)
        {
            try
            {
                byte[] initVectorBytes;
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);

                byte[] saltValueBytes;
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                byte[] cipherTextBytes;
                cipherTextBytes = Convert.FromBase64String(cipherText);

                PasswordDeriveBytes password;
                password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                byte[] keyBytes;
                keyBytes = password.GetBytes((int)(keySize / (double)8));

                RijndaelManaged symmetricKey;
                symmetricKey = new RijndaelManaged
                {
                    Mode = CipherMode.CBC
                };

                ICryptoTransform decryptor;
                decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                MemoryStream memoryStream;
                memoryStream = new MemoryStream(cipherTextBytes);

                CryptoStream cryptoStream;
                cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] plainTextBytes;
                plainTextBytes = new byte[cipherTextBytes.Length + 1];

                int decryptedByteCount;
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                string plainText;
                plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                //Decrypt = plainText;
                //return Decrypt;
                return plainText;
            }
            catch (System.Exception)
            { return "gpE-MU0awVU"; }
        }
        public static string ToTitle(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                return string.Format(textInfo.ToTitleCase(data.ToLower()));
            }
            return data;
        }

        //public static int GetIndex(Spinner spinner, string myString)
        //{
        //    int index = 0;

        //    for (int i = 0; i < spinner.Count; i++)
        //    {
        //        if (spinner.GetItemAtPosition(i).Equals(myString))
        //        {
        //            index = i;
        //            break;
        //        }
        //    }
        //    return index;
        //}



        //public static void MessageBox(Activity act, string title, string msg)
        //{
        //    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(act);
        //    AlertDialog alert = dialog.Create();
        //    alert.SetTitle(title);
        //    alert.SetMessage(msg);
        //    //alert.SetIcon(Resource.Drawable.alert);
        //    alert.SetButton("OK", (c, ev) =>
        //    {
        //    });
        //    //alert.SetButton2("CANCEL", (c, ev) => { });
        //    alert.Show();
        //}

        //public static void MessageBoxCloseAct(Activity act, string title, string msg)
        //{
        //    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(act);
        //    AlertDialog alert = dialog.Create();
        //    alert.SetTitle(title);
        //    alert.SetMessage(msg);
        //    //alert.SetIcon(Resource.Drawable.alert);
        //    alert.SetButton("OK", (c, ev) =>
        //    {
        //        act.Finish();
        //    });
        //    //alert.SetButton2("CANCEL", (c, ev) => { });
        //    alert.Show();
        //}

        //public static void MessageBoxEndApp(Activity act, string title, string msg)
        //{
        //    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(act);
        //    AlertDialog alert = dialog.Create();
        //    alert.SetTitle(title);
        //    alert.SetMessage(msg);
        //    //alert.SetIcon(Resource.Drawable.alert);
        //    alert.SetButton("OK", (c, ev) =>
        //    {
        //        JavaSystem.Exit(0);
        //    });
        //    //alert.SetButton2("CANCEL", (c, ev) => { });
        //    alert.Show();
        //}

        //public static void MessageBoxCloseActivity(Activity act, string title, string msg)
        //{
        //    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(act);
        //    AlertDialog alert = dialog.Create();
        //    alert.SetTitle(title);
        //    alert.SetMessage(msg);
        //    //alert.SetIcon(Resource.Drawable.alert);
        //    alert.SetButton("OK", (c, ev) =>
        //    {
        //        act.Finish();
        //    });
        //    //alert.SetButton2("CANCEL", (c, ev) => { });
        //    alert.Show();
        //}


        //public static string Convert2ProperNumber(string strNum)
        //{
        //    string strRetVal = "";
        //    try
        //    {
        //        string strLastChar = strNum.Substring(strNum.Length - 1);
        //        strRetVal = strLastChar switch
        //        {
        //            "1" => strNum[0..^1] + "0",
        //            "2" => strNum[0..^1] + "00",
        //            "3" => strNum[0..^1] + "000",
        //            "4" => strNum[0..^1] + "0000",
        //            "5" => strNum[0..^1] + "00000",
        //            "6" => strNum[0..^1] + "000000",
        //            "7" => strNum[0..^1] + "0000000",
        //            "8" => strNum[0..^1] + "00000000",
        //            "9" => strNum[0..^1] + "000000000",
        //            _ => strNum,
        //        };
        //    }
        //    catch (System.Exception ex)
        //    { throw ex; }
        //    strRetVal = MyOutput(ValF(strRetVal));
        //    return strRetVal;
        //}

        //public static void ExportBitmapAsPNG(Bitmap bitmap, string filename)
        //{
        //    try
        //    {
        //        //var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        //        var sdCardPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        //        sdCardPath = System.IO.Path.Combine(sdCardPath, "/mnt/sdcard/DPM/Images");
        //        //var filePath = System.IO.Path.Combine(sdCardPath, "test.png");
        //        var filePath = System.IO.Path.Combine(sdCardPath, filename);
        //        var stream = new FileStream(filePath, FileMode.Create);
        //        bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
        //        stream.Close();
        //    }
        //    catch (System.Exception ex)
        //    { throw ex; }
        //}

        //public static string ExportBitmapAsJPEG(Bitmap bitmap, string filename)
        //{
        //    try
        //    {
        //        //var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        //        //var sdCardPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        //        //sdCardPath = System.IO.Path.Combine(sdCardPath, "/mnt/sdcard/DPM/Images"); 
        //        var sdCardPath = CDataAccess.pathToImage;
        //        //var filePath = System.IO.Path.Combine(sdCardPath, "test.png");
        //        var filePath = System.IO.Path.Combine(sdCardPath, filename);
        //        var stream = new FileStream(filePath, FileMode.Create);
        //        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
        //        stream.Close();
        //        return filePath;
        //    }
        //    catch (System.Exception ex)
        //    { throw ex; }
        //}

        //public static string Upload(string uriStr, string FileName, string TypeName = "", bool IsDeleteFile = false)
        //{
        //    string GFileName, tempFileName, NewFileName;
        //    try
        //    {
        //        //uriStr = uriStr.Replace("182.71.11.91:82", "182.71.11.91:81");
        //        if (FileName != "")
        //        {
        //            FileName = FileName.Replace("+", "").Replace("-", "");
        //            System.Net.WebClient myWebClient = new System.Net.WebClient();

        //            if (TypeName.ToString().Contains("/") == true)
        //                TypeName = TypeName.Replace("/", "");
        //            string[] s;
        //            if (FileName.Contains("http://"))
        //                return FileName;
        //            else
        //                s = FileName.Split(new Char[] { '/' });
        //            //GFileName = s[s.Length - 1];     // Extracting the Actual FileName, from its Complete Path. 
        //            GFileName = s[^1];     // Extracting the Actual FileName, from its Complete Path. 
        //            tempFileName = GFileName;
        //            string gFileNameWithoutExtn = GFileName.Substring(0, GFileName.IndexOf("."));
        //            //string gFileNameExtn = GFileName.Substring(GFileName.IndexOf("."), GFileName.Length - GFileName.IndexOf("."));
        //            string gFileNameExtn = GFileName[GFileName.IndexOf(".")..];

        //            GFileName = TypeName + gFileNameWithoutExtn + "-" + DateTime.Now.ToString("yyyyxMMxddxhhxmmxssffff") + gFileNameExtn;
        //            NewFileName = FileName.Replace(tempFileName, GFileName);
        //            System.IO.File.Copy(FileName, NewFileName);
        //            System.IO.FileInfo f = new System.IO.FileInfo(FileName);
        //            long tsize;
        //            tsize = f.Length;
        //            System.Threading.Thread.Sleep(100);
        //            byte[] responseArray = myWebClient.UploadFile(uriStr, System.Convert.ToString(FileName.Replace(tempFileName, GFileName)));
        //            System.Threading.Thread.Sleep(100);
        //            return uriStr.Replace("upload1.aspx", GFileName);
        //        }
        //        else
        //        { return ""; }
        //    }
        //    catch (WebException ex)
        //    {
        //        CGlobalVariables.strProgress = ex.Message;
        //        return "";
        //    }
        //    catch (System.Exception)
        //    { return ""; }
        //}

        public static bool IsOnline()
        {
            try
            {
                //var cm = (ConnectivityManager)GetSystemService(ConnectivityService);
                //return cm.ActiveNetworkInfo == null ? false : cm.ActiveNetworkInfo.IsConnected;
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Local || current == NetworkAccess.Internet)
                { return true; }
                else
                { return false; }
            }
            catch (System.Exception)
            { return false; }
        }

        public static string MyOutput(this decimal number)
        {
            return number.ToString("#,#.##", new CultureInfo(0x0439));
        }

        private static string MyOutput(float number)
        {
            return number.ToString("#,#.##", new CultureInfo(0x0439));
        }

        public static string MyOutput(this int number)
        {
            return number.ToString("#,#", new CultureInfo(0x0439));
        }

        //public static string UpdateSyncDate()
        //{
        //    try
        //    {
        //        int nSyncCnt = CDataAccess.GetLogCount();
        //        string[] strLastSyncDate = CMessage.dtLastSyndDate.Split('\n');
        //        if (strLastSyncDate.Length < 2)
        //        {
        //            strLastSyncDate = new string[2];
        //            strLastSyncDate[1] = DateTime.Now.ToString("dd-MMM-yyyy");
        //        }
        //        strLastSyncDate[0] = "Last Sync: (" + nSyncCnt + ") ";
        //        if (strLastSyncDate.Length > 1)
        //            CMessage.dtLastSyndDate = strLastSyncDate[0] + "\n" + strLastSyncDate[1];
        //        return CMessage.dtLastSyndDate;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //Toast.MakeText(this, ex.Message + " In UpdateSyncDate", ToastLength.Short).Show();
        //        throw ex;
        //    }
        //}

        public static void GetFile(string strUri, string strLoc)
        {
            try
            {
                int LastIdx = strUri.LastIndexOf('/');
                string fName = strUri.Substring(LastIdx + 1);
                LastIdx = GlobalVariables.UploadFilePath.LastIndexOf('/');
                string strLocUri = GlobalVariables.UploadFilePath.Substring(0, LastIdx + 1);
                strLocUri += fName;
                //string strIp = strUri.Substring(0, strUri.IndexOf("/upload"));
                //string strIpImgServer = CGlobalVariables.UploadFilePath.Substring(0, CGlobalVariables.UploadFilePath.IndexOf("/upload"));
                //if (strIp == strIpImgServer)
                //{
                System.Net.WebClient webClient = new System.Net.WebClient();
                webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Completed);
                //webClient.DownloadFile(new Uri(strUri), strLoc);
                webClient.DownloadFile(new Uri(strLocUri), strLoc);
                //}
                //else
                //{
                //    CGlobalVariables.strProgress = "Image server in database and DPM doesnot match.\n Image cannot be downloaded.";
                //    System.Threading.Thread.Sleep(1000);
                //}
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Message != "The remote server returned an error: (404) Not Found.")
                    throw ex;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private static void Completed(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //Console.WriteLine("ERROR: " + e.Error.Message);
        }

        
    }

}
