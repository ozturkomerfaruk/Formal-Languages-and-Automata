using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Instagrammer
{
    public partial class FormTakipBilgiSistemi : Form
    {
        public FormTakipBilgiSistemi()
        {
            InitializeComponent();
        }

        public string kullaniciAdi { get; set; }
        public string parola { get; set; }

        private void FormTakipciAnalizEkranı_Load(object sender, EventArgs e)
        {
            txtUserNameForm2.Text = kullaniciAdi;

            txtUserNameForm2.Enabled = false;
            txtName.Enabled = false;
            txtBiyografi.Enabled = false;
            txtGonderiSayisi.Enabled = false;
            txtTakipciSayisi.Enabled = false;
            txtTakipSayisi.Enabled = false;
            txtTakipEtmeyenlerinSayisi.Enabled = false;
            txtHayranlarinSayisi.Enabled = false;

            lboxEnSonTakipciListesi.IntegralHeight = true;
            lboxEnSonTakipListesi.SelectionMode = SelectionMode.MultiExtended;
            lboxEnSonTakipciListesi.SelectionMode = SelectionMode.MultiExtended;

            lboxHastagKullanici.IntegralHeight = true;
            lboxHastagKullanici.SelectionMode = SelectionMode.MultiExtended;
        }

        private void FormTakipciAnalizEkranı_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden emin misiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            Environment.Exit(0);
        }
        
        private void btnGetir_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {    
                driver.Navigate().GoToUrl("https://www.instagram.com");
            }
            catch
            {
                MessageBox.Show("İnternete bağlanılamadı!");
            }
            Thread.Sleep(2000);

            try
            {
                //Not: Aynı şekilde email ve telefon numarası ile girişte eklenmeli!
                IWebElement userName = driver.FindElement(By.Name("username"));       
                userName.SendKeys(kullaniciAdi);
                IWebElement password = driver.FindElement(By.Name("password"));
                password.SendKeys(parola);
                IWebElement loginBtn = driver.FindElement(By.CssSelector(".sqdOP.L3NKy.y3zKF"));
                loginBtn.Click();
            }
            catch 
            {
                MessageBox.Show("Kullanıcı Adı veya Parola Yanlış.");               
            } 
            Thread.Sleep(2500);
            try
            {
                driver.Navigate().GoToUrl("https://www.instagram.com/" + kullaniciAdi +"/");
            }
            catch
            {
                MessageBox.Show("Profile Yönlendirelemedi!");
            }
            Thread.Sleep(2200);

            List<string> InstagrammerVerileri = new List<string>(); //
            IWebElement PostsSayisi = driver.FindElement(By.CssSelector("#react-root > section > main > div > header > section > ul > li:nth-child(1) > span > span"));
            IWebElement followersSayisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/ul/li[2]/a/span"));
            IWebElement followingSayisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/ul/li[3]/a/span"));
  
            InstagrammerVerileri.Add(PostsSayisi.Text);
            InstagrammerVerileri.Add(followersSayisi.Text);
            InstagrammerVerileri.Add(followingSayisi.Text);
            txtGonderiSayisi.Text = InstagrammerVerileri[0]; //339 takipçi boşluktan sonrasını almıcaz.
            txtTakipciSayisi.Text = InstagrammerVerileri[1];
            txtTakipSayisi.Text = InstagrammerVerileri[2];

            //Burada bir hata alıyorum. Mesela instagramdan gelen isim null ise ismin boş olduğunu nasıl göstereceğim?
            //veya aynı şekilde biyografi boş ise nasıl göstereceğim?
         
            IWebElement NameBilgisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/div[2]/h1"));
            InstagrammerVerileri.Add(NameBilgisi.Text);
            txtName.Text = InstagrammerVerileri[3];

            IWebElement BiyogrofiBilgisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/div[2]/span"));
            InstagrammerVerileri.Add(BiyogrofiBilgisi.Text);
            txtBiyografi.Text = InstagrammerVerileri[4];
           
            Thread.Sleep(500);

            List<Instagrammer> instagrammerFollowers = new List<Instagrammer>();
            List<Instagrammer> DMTAKIPCI = new List<Instagrammer>();    
            try // Takipçi Listesi
            {
                IWebElement followerLink = driver.FindElement(By.CssSelector("#react-root > section > main > div > header > section > ul > li:nth-child(2) > a > span"));
                followerLink.Click();
                Thread.Sleep(2500);
                //ScrollDown Start
                //isgrP
                string jsCommand = "sayfa = document.querySelector('.isgrP');" +
                    "sayfa.scrollTo(0, sayfa.scrollHeight);" +
                    "var sayfaSonu = sayfa.scrollHeight;" +
                    "return sayfaSonu;";

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver; //driver ile ilişkilendirme
                var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                while (true)
                {
                    var son = sayfaSonu;
                    Thread.Sleep(500);
                    sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                    if (son == sayfaSonu)
                        break;
                }
                //ScrollDown END

                //Takipçi Listele Start
                IReadOnlyCollection<IWebElement> followers = driver.FindElements(By.CssSelector(".FPmhX.notranslate._0imsa"));
                
                int i = 1;
                foreach (IWebElement follower in followers)
                {
                    instagrammerFollowers.Add(new Instagrammer() { FollowersNumber = i, FollowersUserName = follower.Text });
                    i++;
                }
                //Takipçi Listeleme END  
                lboxEnSonTakipciListesi.DataSource = instagrammerFollowers;
                lboxEnSonTakipciListesi.DisplayMember = "FollowersUserName";
                lboxEnSonTakipciListesi.ValueMember = "FollowersNumber";

                checkedLBoxDMListesi.Items.Clear();
                int k = 1;
                foreach (IWebElement follower in followers)
                {
                    DMTAKIPCI.Add(new Instagrammer() { takipcilerineDMGondermeINTEGER = k, takipcilerineDMGondermeSTRING = follower.Text });
                    k++;
                }
                //Takipçi Listeleme END  
                checkedLBoxDMListesi.DataSource = DMTAKIPCI;
                checkedLBoxDMListesi.DisplayMember = "takipcilerineDMGondermeSTRING";
                checkedLBoxDMListesi.ValueMember = "takipcilerineDMGondermeINTEGER";

                Thread.Sleep(200);
                IWebElement CloseFollowersBtn = driver.FindElement(By.CssSelector("body > div.RnEpo.Yx5HN > div > div > div:nth-child(1) > div > div:nth-child(3) > button > div > svg"));
                CloseFollowersBtn.Click();
                Thread.Sleep(500);
            }
            catch
            {
                return;
            }
            

            List<Instagrammer> instagrammerFollowing = new List<Instagrammer>();
            try //takip ettiklerin listesi
            {
                IWebElement followingLink = driver.FindElement(By.CssSelector("#react-root > section > main > div > header > section > ul > li:nth-child(3) > a > span"));
                followingLink.Click();
                Thread.Sleep(2500);
                //ScrollDown Start
                //isgrP
                string jsCommandTakipci = "sayfa = document.querySelector('.isgrP');" +
                    "sayfa.scrollTo(0, sayfa.scrollHeight);" +
                    "var sayfaSonu = sayfa.scrollHeight;" +
                    "return sayfaSonu;";

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver; //driver ile ilişkilendirme
                var sayfaSonuTakipci = Convert.ToInt32(js.ExecuteScript(jsCommandTakipci));
                while (true)
                {
                    var sonTakipci = sayfaSonuTakipci;
                    Thread.Sleep(500);
                    sayfaSonuTakipci = Convert.ToInt32(js.ExecuteScript(jsCommandTakipci));
                    if (sonTakipci == sayfaSonuTakipci)
                        break;
                }
                //ScrollDown END

                //Takip Ettiklerin Start
                IReadOnlyCollection<IWebElement> followings = driver.FindElements(By.CssSelector(".FPmhX.notranslate._0imsa"));
               
                int j = 1;
                foreach (IWebElement following in followings)
                {
                    instagrammerFollowing.Add(new Instagrammer() { FollowingNumber = j, FollowingUserName = following.Text });
                    j++;
                }
                //Takip Ettiklerin Start   
                lboxEnSonTakipListesi.DataSource = instagrammerFollowing;
                lboxEnSonTakipListesi.DisplayMember = "FollowingUserName";
                lboxEnSonTakipListesi.ValueMember = "FollowingNumber";

                Thread.Sleep(200);
                IWebElement CloseFollowersBtn = driver.FindElement(By.CssSelector("body > div.RnEpo.Yx5HN > div > div > div:nth-child(1) > div > div:nth-child(3) > button > div > svg"));
                CloseFollowersBtn.Click();
                Thread.Sleep(500);
                Thread.Sleep(200);
                IWebElement CloseFollowingBtn = driver.FindElement(By.CssSelector("body > div.RnEpo.Yx5HN > div > div > div:nth-child(1) > div > div:nth-child(3) > button > div > svg"));
                CloseFollowingBtn.Click();
                Thread.Sleep(500);
            }
            catch 
            {
                return;
            }


            //hayranlarının sayısı
            var hayranCounter = 0;
            for (int i = 0; i < instagrammerFollowers.Count; i++)
            {
                Instagrammer instagrammer = instagrammerFollowers[i];
                foreach (var item in instagrammerFollowing)
                {
                    hayranCounter = (item == instagrammer) ? hayranCounter + 1 : hayranCounter;
                }
            }
            txtHayranlarinSayisi.Text = hayranCounter.ToString();

            //takip etmeyenlerin sayısı
            var hainCounter = 0;
            for (int i = 0; i < instagrammerFollowing.Count; i++)
            {
                Instagrammer instagrammer = instagrammerFollowing[i];
                foreach (var item in instagrammerFollowers)
                {
                    hainCounter = (item == instagrammer) ? hainCounter + 1 : hainCounter;
                }
            }
            txtTakipEtmeyenlerinSayisi.Text = hainCounter.ToString();

            Console.WriteLine(hayranCounter);
            Console.WriteLine(hainCounter);
        }

        private void txtHashtag_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {
                driver.Navigate().GoToUrl("https://www.instagram.com/" + kullaniciAdi + "/");
            }
            catch
            {
                MessageBox.Show("Profile Yönlendirelemedi!");
            }
            Thread.Sleep(1000);

            List<string> InstagrammerVerileri = new List<string>(); //
            IWebElement PostsSayisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/ul/li[1]/a/span"));
            IWebElement followersSayisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/ul/li[2]/a/span"));
            IWebElement followingSayisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/ul/li[3]/a/span"));
            IWebElement NameBilgisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/div[2]/h1"));
            IWebElement BiyogrofiBilgisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/div[2]/span"));
            InstagrammerVerileri.Add(PostsSayisi.Text);
            InstagrammerVerileri.Add(followersSayisi.Text);
            InstagrammerVerileri.Add(followingSayisi.Text);
            InstagrammerVerileri.Add(NameBilgisi.Text);
            InstagrammerVerileri.Add(BiyogrofiBilgisi.Text);

            txtGonderiSayisi.Text = InstagrammerVerileri[0]; //339 takipçi boşluktan sonrasını almıcaz.

            txtTakipciSayisi.Text = InstagrammerVerileri[1];

            txtTakipSayisi.Text = InstagrammerVerileri[2];

            txtName.Text = InstagrammerVerileri[3];

            txtBiyografi.Text = InstagrammerVerileri[4];

            //driver.Close(); //driver kapatmada sorun yaşıyorum. Anlamadım sebebini.
        }

        private void txtBoxTakipciSecme_TextChanged(object sender, EventArgs e)
        {
            //buraya yazıldıkça checkedboxlist de gözükmesi lazım.
            //ve birde mümkünse seçili olan checkedbox listenin en üstte gözükmesi lazım. Daha havalı olur :)
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            //textboxa yazılanlara ekle butonuna tıklandığında checkedlistbox ta seçili hale gelmesi lazım
            checkedLBoxDMListesi.Items.Add(txtBoxTakipciSecme.Text);
            txtBoxTakipciSecme.Text = "";
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < checkedLBoxDMListesi.Items.Count; i++)
                checkedLBoxDMListesi.SetItemCheckState(i, CheckState.Unchecked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkedLBoxDMListesi.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool state = false;
            for (int i = 0; i < checkedLBoxDMListesi.Items.Count; i++)
                checkedLBoxDMListesi.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkedLBoxDMListesi.CheckedItems.OfType<string>().ToList().ForEach(checkedLBoxDMListesi.Items.Remove);
        }

        private void btnHepsiniSec_Click(object sender, EventArgs e)
        {
            bool state = true;
            for (int i = 0; i < checkedLBoxDMListesi.Items.Count; i++)
                checkedLBoxDMListesi.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));
        }

        private void btnDMGonder_Click(object sender, EventArgs e)
        {
            //Giriş Yapldı mı diye sorgulaması lazım normalde. Ancak bunu sonra düzeltirim. Ben sıfırdan giriş yaptırım şimdi
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {
                driver.Navigate().GoToUrl("https://www.instagram.com");
            }
            catch
            {
                MessageBox.Show("İnternete bağlanılamadı!");
            }
            Thread.Sleep(2000);

            try
            {
                //Not: Aynı şekilde email ve telefon numarası ile girişte eklenmeli!
                IWebElement userName = driver.FindElement(By.Name("username"));
                userName.SendKeys(kullaniciAdi);
                IWebElement password = driver.FindElement(By.Name("password"));
                password.SendKeys(parola);
                IWebElement loginBtn = driver.FindElement(By.CssSelector(".sqdOP.L3NKy.y3zKF"));
                loginBtn.Click();
            }
            catch
            {
                return;
            }
            Thread.Sleep(3000);
            IWebElement DMGiris = driver.FindElement(By.CssSelector("#react-root > section > nav > div._8MQSO.Cx7Bp > div > div > div.ctQZg > div > div:nth-child(2) > a"));
            DMGiris.Click();
            Thread.Sleep(2000);

            IWebElement BildirimleriSimdiDegil = driver.FindElement(By.CssSelector("body > div.RnEpo.Yx5HN > div > div > div > div.mt3GC > button.aOOlW.HoLwm"));
            BildirimleriSimdiDegil.Click();
            Thread.Sleep(2000);

            IWebElement YeniMesaj = driver.FindElement(By.CssSelector("#react-root > section > div > div.Igw0E.IwRSH.eGOV_._4EzTm > div > div > div.oNO81 > div.S-mcP > div > div._2NzhO.EQ1Mr > button > div > svg"));
            YeniMesaj.Click();
            Thread.Sleep(2000);

            //Normalde buraya Kime kısmına checkedlistboxtaki kişileri aktarmam lazım. Ancak checkedlistboxı halledemedim.
            IWebElement kime = driver.FindElement(By.CssSelector("body > div.RnEpo.Yx5HN > div > div > div.Igw0E.IwRSH.eGOV_.vwCYk.i0EQd > div.TGYkm > div > div.HeuYH > input"));
            Thread.Sleep(1000);

            List<string> secilicbox = new List<string>();
            CheckedListBox.CheckedItemCollection secilenler = checkedLBoxDMListesi.CheckedItems;
            int seliciboxCount = 0;
            foreach (var item in secilenler)
            {
                secilicbox.Add(item.ToString());
                seliciboxCount++;
            }
            
            for (int i = 0; i < seliciboxCount; i++)
            {
                kime.SendKeys(secilicbox[i]);
                Thread.Sleep(1500);
                IWebElement kimeSeciliTikla = driver.FindElement(By.CssSelector("body > div.RnEpo.Yx5HN > div > div > div.Igw0E.IwRSH.eGOV_.vwCYk.i0EQd > div.Igw0E.IwRSH.eGOV_.vwCYk._3wFWr > div:nth-child(1) > div > div.Igw0E.rBNOH.YBx95.ybXk5._4EzTm.soMvl > button"));
                kimeSeciliTikla.Click();
                Thread.Sleep(1500);
            }

            IWebElement Next = driver.FindElement(By.CssSelector("body > div.RnEpo.Yx5HN > div > div > div:nth-child(1) > div > div:nth-child(3) > div > button"));
            Next.Click();
            Thread.Sleep(2000);
            //Eğer bir resim gönderilecekse ona göre tam burada bir ayar çekilmeli.

            IWebElement DMYeri = driver.FindElement(By.CssSelector("#react-root > section > div > div.Igw0E.IwRSH.eGOV_._4EzTm > div > div > div.DPiy6.Igw0E.IwRSH.eGOV_.vwCYk > div.uueGX > div > div.Igw0E.IwRSH.eGOV_._4EzTm > div > div > div.Igw0E.IwRSH.eGOV_.vwCYk.ItkAi > textarea"));
            DMYeri.SendKeys(richtxtDM.Text);
            Thread.Sleep(2500);

            IWebElement Send = driver.FindElement(By.XPath("/html/body/div[1]/section/div/div[2]/div/div/div[2]/div[2]/div/div[2]/div/div/div[3]/button"));
            Send.Click();
            Thread.Sleep(500);
        }

        private void tabOzelMesaj_Click(object sender, EventArgs e)
        {
            //yanlışlıkla tıklandı, geç
        }

        private void btnZoomFactorPlus_Click(object sender, EventArgs e)
        {
            try
            {
                richtxtDM.ZoomFactor += 0.5f;
            }
            catch 
            {
                MessageBox.Show("Daha fazla büyütemezsin.");
            }
            
        }

        private void btnZoomFactorMinus_Click(object sender, EventArgs e)
        {      
            try
            {
                richtxtDM.ZoomFactor -= 0.5f;
            }
            catch 
            {
                MessageBox.Show("Daha fazla küçültemezsin.");
            }
        }

        private void btnLeftAlign_Click(object sender, EventArgs e)
        {
            richtxtDM.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void btnCenterAlign_Click(object sender, EventArgs e)
        {
            richtxtDM.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void btnRightAlign_Click(object sender, EventArgs e)
        {
            richtxtDM.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            richtxtDM.Redo();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            richtxtDM.Undo();
        }

        private void btnRTBKaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            
            saveFile1.DefaultExt = "*.rtf";
            saveFile1.Filter = "RTF Files|*.rtf";
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               saveFile1.FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                richtxtDM.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void btnRTBYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile1 = new OpenFileDialog();

            openFile1.DefaultExt = "*.rtf";
            openFile1.Filter = "RTF Files|*.rtf";

            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               openFile1.FileName.Length > 0)
            {
                // Load the contents of the file into the RichTextBox.
                richtxtDM.LoadFile(openFile1.FileName);
            }
        }

        private void checkedLBoxTakipciListen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFotograflariGetir_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {
                driver.Navigate().GoToUrl("https://www.instagram.com");
            }
            catch
            {
                MessageBox.Show("İnternete bağlanılamadı!");
            }
            Thread.Sleep(2000);

            try
            {
                //Not: Aynı şekilde email ve telefon numarası ile girişte eklenmeli!
                IWebElement userName = driver.FindElement(By.Name("username"));
                userName.SendKeys(kullaniciAdi);
                IWebElement password = driver.FindElement(By.Name("password"));
                password.SendKeys(parola);
                IWebElement loginBtn = driver.FindElement(By.CssSelector(".sqdOP.L3NKy.y3zKF"));
                loginBtn.Click();
            }
            catch
            {
                MessageBox.Show("Kullanıcı Adı veya Parola Yanlış.");
                driver.Close();

            }
            Thread.Sleep(2500);
            try
            {
                driver.Navigate().GoToUrl("https://www.instagram.com/" + kullaniciAdi + "/");
            }
            catch
            {
                MessageBox.Show("Profile Yönlendirelemedi!");
            }
            Thread.Sleep(2200);

            for (int i = 1; i <= 4; i++)
            {
                IWebElement fotograflar = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/div[2]/article/div/div/div[1]/div["+i+"]/a/div"));
                
            }
        }

        private void btnHashtagAra_Click(object sender, EventArgs e)
        {
            
            
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {
                driver.Navigate().GoToUrl("https://www.instagram.com");
            }
            catch
            {
               
            }
            Thread.Sleep(2000);

            try
            {

                IWebElement userName = driver.FindElement(By.Name("username"));
                userName.SendKeys(kullaniciAdi);
                IWebElement password = driver.FindElement(By.Name("password"));
                password.SendKeys(parola);
                IWebElement loginBtn = driver.FindElement(By.CssSelector(".sqdOP.L3NKy.y3zKF"));
                loginBtn.Click();
            }
            catch
            {
                
            }
            Thread.Sleep(2500);

            string hashtagGirmeYeri = "";
            hashtagGirmeYeri = txtHashtag.Text;
            //Burada kullanıcı girerken hashtag yazısını silerse ona otomatik olacak şekilde varmış gibi ilk karakterine # eklenmesi gerekmektedir.

            try
            {
                driver.Navigate().GoToUrl("https://www.instagram.com/explore/tags/" + hashtagGirmeYeri + "/");
                Thread.Sleep(5000);
            }
            catch 
            {
                MessageBox.Show("Hashtag Arama Başarısız Oldu!");
            }

            //postsayisi

            /*try
            { 
                IWebElement hashtagPostSayisi = driver.FindElement(By.XPath("/html/body/div[1]/section/main/header/div[2]/div/div[2]/span/span"));
                List<string> InstagramHashtagVerileri = new List<string>();
                InstagramHashtagVerileri.Add(hashtagPostSayisi.Text);
                txtHashtagPostSayisi.Text = InstagramHashtagVerileri[0];
                Thread.Sleep(1000);
            }
            catch 
            {
                MessageBox.Show("Post Sayisi Gösterilemedi!");
            }*/

            txtHashtagPostSayisi.Text = "4";

            IWebElement hashtagFotografTiklama = driver.FindElement(By.ClassName("_9AhH0"));
            hashtagFotografTiklama.Click();
            Thread.Sleep(1000);

            IWebElement hashtagFotografBegeniSayisi = driver.FindElement(By.XPath("/html/body/div[5]/div[2]/div/article/div[3]/section[2]/div/div/a"));
            hashtagFotografBegeniSayisi.Click();
            Thread.Sleep(1000);

            //--------------------------------------------------------

            
            try // Hashtag Kullanıcı Listesi
            {
                //ScrollDown Start
                string jsCommand = "sayfa = document.querySelector('body > div.RnEpo.Yx5HN > div > div > div.Igw0E.IwRSH.eGOV_.vwCYk.i0EQd > div');" +
                    "sayfa.scrollTo(0, sayfa.scrollHeight);" +
                    "var sayfaSonu = sayfa.scrollHeight;" +
                    "return sayfaSonu;";    

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver; //driver ile ilişkilendirme
                var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                while (true)
                {
                    var son = sayfaSonu;
                    Thread.Sleep(1000);
                    sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                    if (son == sayfaSonu)
                        break;
                }
                //ScrollDown END

                //Hashtag Kullanıcı Listele Start
                IReadOnlyCollection<IWebElement> kullaniciSayisi = driver.FindElements(By.CssSelector(".FPmhX.notranslate.MBL3Z"));
                List<hashtagInstagrammer> hashtagKullaniciSayisi = new List<hashtagInstagrammer>();
                List<hashtagInstagrammer> YildimIyiceninListi = new List<hashtagInstagrammer>();

                int sayac = 1;
                foreach (IWebElement kullanici in kullaniciSayisi)
                {
                    hashtagKullaniciSayisi.Add(new hashtagInstagrammer() { hashtagKullaniciSayisiniCekme = sayac, hashtagKullaniciCekme = kullanici.Text });
                    sayac++;              
                }
                lblToplamKullaniciSayisiTEXT.Text = (sayac-1).ToString();

                //Hashtag Kullanıcı Listeleme END  
                lboxHastagKullanici.DataSource = hashtagKullaniciSayisi;
                lboxHastagKullanici.DisplayMember = "hashtagKullaniciCekme";
                lboxHastagKullanici.ValueMember = "hashtagKullaniciSayisiniCekme";

                int osurdugumunSayaci = 1;
                foreach (IWebElement kullanici in kullaniciSayisi)
                {
                    YildimIyiceninListi.Add(new hashtagInstagrammer() { biktimVallaArtik = osurdugumunSayaci, UsandigiminStringi = kullanici.Text });
                    osurdugumunSayaci++;
                }
                checkedLBoxDMListesi.Items.Clear();
                checkedLBoxDMListesi.DataSource = YildimIyiceninListi;
                checkedLBoxDMListesi.DisplayMember = "UsandigiminStringi";
                checkedLBoxDMListesi.ValueMember = "biktimVallaArtik";

                /*object[] objCollection = new object[lboxHastagKullanici.Items.Count];
                lboxHastagKullanici.Items.CopyTo(objCollection, 0);
                checkedLBoxDMListesi.Items.AddRange(objCollection);*/


                Thread.Sleep(1000);

                int sayacCount = 1;
                foreach (var item in kullaniciSayisi)
                {
                    IWebElement loginTakipEt = driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[" + sayacCount + "]/div[3]"));
                    loginTakipEt.Click();
                    Thread.Sleep(1000);
                    sayacCount++;
                }
                
                IWebElement CloseKullaniciSayisi = driver.FindElement(By.CssSelector("body > div.RnEpo.Yx5HN > div > div > div:nth-child(1) > div > div:nth-child(3) > button > div > svg"));
                CloseKullaniciSayisi.Click();
                Thread.Sleep(1000);

                IWebElement CloseFotograf = driver.FindElement(By.XPath("/html/body/div[5]/div[3]/button"));
                CloseFotograf.Click();
                Thread.Sleep(1000);
            }
            catch
            {
                
            }
        }

        private void btnMesajIslemleri_Click(object sender, EventArgs e)
        {
            tabOzelMesaj.Show();
            tabControl.SelectedTab = tabOzelMesaj;
        }

        private void richtxtDM_TextChanged(object sender, EventArgs e)
        {

        }

        private void lboxHastagKullanici_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTakipcilerineDMGonder_Click(object sender, EventArgs e)
        {
            tabOzelMesaj.Show();
            tabControl.SelectedTab = tabOzelMesaj;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ozturkomerfaruk");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/dzeep");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/YahyaBekir");
        }
    }
    class Instagrammer
    {
        public int FollowersNumber { get; set; } //Listte sayaç var.
        public string FollowersUserName { get; set; } //Liste aktarma
        public int FollowingNumber { get; set; } //Listte sayac var
        public string FollowingUserName { get; set; } //Liste aktarma
        public string gonderiSayisi { get; set; } //direk instagramdan veriyi çekme
        public string takipciSayisi { get; set; } //direk instagramdan veriyi çekme
        public string takipEtmeSayisi { get; set; } //direk instagramdan veriyi çekme
        public int takipEttiklerineDMGondermeINTEGER { get; set; }
        public string takipEttiklerineDMGondermeSTRING { get; set; }
        public int takipcilerineDMGondermeINTEGER { get; set; }
        public string takipcilerineDMGondermeSTRING { get; set; }
    }

    class hashtagInstagrammer
    {
        public string hashtagPostSayisiniCekme { get; set; }
        public int hashtagKullaniciSayisiniCekme { get; set; }
        public string hashtagKullaniciCekme { get; set; }
        public int biktimVallaArtik { get; set; }
        public string UsandigiminStringi { get; set; }
    }
}
