namespace sv_Interlocking_Main
{
    public class sv_Interlocking_Main_Class
    {

        public static bool status = false;
        public static string Infor = "";
        public static int SV_Interlocking_Main(string Information)
        {

            Infor = Information;
            SV_InterlockingForm form = new SV_InterlockingForm();
            form.ShowDialog();
            if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                status = true;
            }
            else if (form.DialogResult == System.Windows.Forms.DialogResult.No)
            {
                status = false;
            }

            if (status == true)
            {
                return 0;
            }
            else
            {
                return -1;
            }



        }

    }
}
