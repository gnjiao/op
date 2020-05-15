using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.ToolKit
{
    public static partial class Extensions1
    {
        #region 布尔：NextBool
        public static bool NextBool(this Random random)
        {
            return random.NextDouble() > 0.5;
        }
        #endregion
        #region 枚举: NextEnum
        /// <summary>
        /// 举例:enum Shape { Ellipse, Rectangle, Triangle }
        ///      Shape shape = random.NextEnum<Shape>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T NextEnum<T>(this Random random) where T : struct
        {
            Type type = typeof(T);
            if (type.IsEnum == false) throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }
        #endregion
        #region NextBytes
        /// <summary>
        /// 举例:byte[] data = random.NextBytes(128);
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] NextBytes(this Random random, int length)
        {
            var data = new byte[length];
            random.NextBytes(data);
            return data;
        }
        #endregion
        #region NextUInt16、NextInt16、NextFloat…
        public static UInt16 NextUInt16(this Random random)
        {
            return BitConverter.ToUInt16(random.NextBytes(2), 0);
        }
        public static Int16 NextInt16(this Random random)
        {
            return BitConverter.ToInt16(random.NextBytes(2), 0);
        }
        public static float NextFloat(this Random random)
        {
            return BitConverter.ToSingle(random.NextBytes(4), 0);
        }
        #endregion
        #region 时间日期：NextDateTime
        /// <summary>
        /// 举例:DateTime d = random.NextDateTime(new DateTime(2000, 1, 1), new DateTime(2010, 12, 31));
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
            return new DateTime(ticks);
        }
        public static DateTime NextDateTime(this Random random)
        {
            return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
        }
        #endregion
        #region 随机字符串长度:NextString
        public static string NextString(this Random @this,string inputString, int length=1,bool isUnique=false)
        {
            string newStr = string.Empty;
            while (newStr.Length<length)
            {
                int r = @this.Next(0, inputString.Length);
                if (isUnique)
                {
                    if ( ! newStr.Contains(inputString[r]))
                    {
                        newStr += inputString[r];
                    } 
                }
                else
                {
                    newStr += inputString[r];
                }
            }
            return newStr;
        }
        #endregion
        #region 随机数组元素:OneOf
        public static T OneOf<T>(this Random @this,List<T> values)
        {
            return OneOf<T>(@this,values.ToArray());
        }
        public static T OneOf<T>(this Random @this, params T[] values)
        {
            return values[@this.Next(0,values.Length)];
        }
        #endregion
        #region 数组随机排序:RandomSort
        public static T[] RandomSort<T>(this Random @this,T[] values)
        {
            return NextSubArray<T>(@this,values,values.Length,true);
        }
        public static List<T> RandomSort<T>(this Random @this,List<T> values)
        {
            return NextSubArray<T>(@this,values.ToArray(),values.Count,true).ToList();
        }
        #endregion
        #region 随机数组子集:NextSubArray
        public static T[] NextSubArray<T>(this Random @this,T[] values,int length,bool isUnique=false)
        {
            int len = values.Length;
            List<int> list = new List<int>();
            //获取随机索引列表
            while (list.Count<length)
            {
                int newIndex = @this.Next(0, len);
                if ( (!isUnique) || (!list.Contains(newIndex)) )
                {
                    list.Add(newIndex);
                }
            }
            //索引数组元素
            T[] newArr = new T[length];
            for (int i = 0; i < length; i++)
            {
                newArr[i] = values[list[i]];
            }
            return newArr;
        }
        #endregion
        #region 随机1D数组:Build1DArray
        public static int[] Build1DArray(this Random @this,int minValue, int maxValue, int length, bool unique = false)
        {
            int[] values = new int[length];
            for (int i = 0; i < length; i++)
            {
                if (unique)
                {
                    int temp = @this.Next(minValue, maxValue);
                    if (!values.Contains(temp)) { values[i] = temp; }
                    else { i--; }
                }
                else
                {
                    values[i] = @this.Next(minValue, maxValue);
                }
            }
            return values;
        }
        #endregion
        #region 生成随机中文名字:NextChineseName
        /// <summary>
        /// 生成随机中文名字
        /// </summary>
        /// <returns></returns>
        public static string NextChineseName(this Random @this)
        {
        /// 生成中文名字的姓
        string[] _crabofirstName = new string[] {  "白", "毕", "卞", "蔡", "曹", "岑", "常", "车", "陈", "成" , "程", "池", "邓", "丁", "范", "方", "樊", "费", "冯", "符"
        , "傅", "甘", "高", "葛", "龚", "古", "关", "郭", "韩", "何" , "贺", "洪", "侯", "胡", "华", "黄", "霍", "姬", "简", "江"
        , "姜", "蒋", "金", "康", "柯", "孔", "赖", "郎", "乐", "雷" , "黎", "李", "连", "廉", "梁", "廖", "林", "凌", "刘", "柳"
        , "龙", "卢", "鲁", "陆", "路", "吕", "罗", "骆", "马", "梅" , "孟", "莫", "母", "穆", "倪", "宁", "欧", "区", "潘", "彭"
        , "蒲", "皮", "齐", "戚", "钱", "强", "秦", "丘", "邱", "饶" , "任", "沈", "盛", "施", "石", "时", "史", "司徒", "苏", "孙"
        , "谭", "汤", "唐", "陶", "田", "童", "涂", "王", "危", "韦" , "卫", "魏", "温", "文", "翁", "巫", "邬", "吴", "伍", "武"
        , "席", "夏", "萧", "谢", "辛", "邢", "徐", "许", "薛", "严" , "颜", "杨", "叶", "易", "殷", "尤", "于", "余", "俞", "虞"
        , "元", "袁", "岳", "云", "曾", "詹", "张", "章", "赵", "郑" , "钟", "周", "邹", "朱", "褚", "庄", "卓" };

        /// 用于生成中文名字的后面两位
        string _lastName = "匕刁丐歹戈夭仑讥冗邓艾夯凸卢叭叽皿凹囚矢乍尔冯玄邦迂邢芋芍吏夷吁吕吆" +
                           "屹廷迄臼仲伦伊肋旭匈凫妆亥汛讳讶讹讼诀弛阱驮驯纫玖玛韧抠扼汞扳抡坎坞抑拟抒芙芜苇芥芯芭杖杉巫" +
                           "杈甫匣轩卤肖吱吠呕呐吟呛吻吭邑囤吮岖牡佑佃伺囱肛肘甸狈鸠彤灸刨庇吝庐闰兑灼沐沛汰沥沦汹沧沪忱" +
                           "诅诈罕屁坠妓姊妒纬玫卦坷坯拓坪坤拄拧拂拙拇拗茉昔苛苫苟苞茁苔枉枢枚枫杭郁矾奈奄殴歧卓昙哎咕呵" +
                           "咙呻咒咆咖帕账贬贮氛秉岳侠侥侣侈卑刽刹肴觅忿瓮肮肪狞庞疟疙疚卒氓炬沽沮泣泞泌沼怔怯宠宛衩祈诡" +
                           "帚屉弧弥陋陌函姆虱叁绅驹绊绎契贰玷玲珊拭拷拱挟垢垛拯荆茸茬荚茵茴荞荠荤荧荔栈柑栅柠枷勃柬砂泵" +
                           "砚鸥轴韭虐昧盹咧昵昭盅勋哆咪哟幽钙钝钠钦钧钮毡氢秕俏俄俐侯徊衍胚胧胎狰饵峦奕咨飒闺闽籽娄烁炫" +
                           "洼柒涎洛恃恍恬恤宦诫诬祠诲屏屎逊陨姚娜蚤骇耘耙秦匿埂捂捍袁捌挫挚捣捅埃耿聂荸莽莱莉莹莺梆栖桦" +
                           "栓桅桩贾酌砸砰砾殉逞哮唠哺剔蚌蚜畔蚣蚪蚓哩圃鸯唁哼唆峭唧峻赂赃钾铆氨秫笆俺赁倔殷耸舀豺豹颁胯" +
                           "胰脐脓逛卿鸵鸳馁凌凄衷郭斋疹紊瓷羔烙浦涡涣涤涧涕涩悍悯窍诺诽袒谆祟恕娩骏琐麸琉琅措捺捶赦埠捻" +
                           "掐掂掖掷掸掺勘聊娶菱菲萎菩萤乾萧萨菇彬梗梧梭曹酝酗厢硅硕奢盔匾颅彪眶晤曼晦冕啡畦趾啃蛆蚯蛉蛀" +
                           "唬啰唾啤啥啸崎逻崔崩婴赊铐铛铝铡铣铭矫秸秽笙笤偎傀躯兜衅徘徙舶舷舵敛翎脯逸凰猖祭烹庶庵痊阎阐" +
                           "眷焊焕鸿涯淑淌淮淆渊淫淳淤淀涮涵惦悴惋寂窒谍谐裆袱祷谒谓谚尉堕隅婉颇绰绷综绽缀巢琳琢琼揍堰揩" +
                           "揽揖彭揣搀搓壹搔葫募蒋蒂韩棱椰焚椎棺榔椭粟棘酣酥硝硫颊雳翘凿棠晰鼎喳遏晾畴跋跛蛔蜒蛤鹃喻啼喧" +
                           "嵌赋赎赐锉锌甥掰氮氯黍筏牍粤逾腌腋腕猩猬惫敦痘痢痪竣翔奠遂焙滞湘渤渺溃溅湃愕惶寓窖窘雇谤犀隘" +
                           "媒媚婿缅缆缔缕骚瑟鹉瑰搪聘斟靴靶蓖蒿蒲蓉楔椿楷榄楞楣酪碘硼碉辐辑频睹睦瞄嗜嗦暇畸跷跺蜈蜗蜕蛹" +
                           "嗅嗡嗤署蜀幌锚锥锨锭锰稚颓筷魁衙腻腮腺鹏肄猿颖煞雏馍馏禀痹廓痴靖誊漓溢溯溶滓溺寞窥窟寝褂裸谬" +
                           "媳嫉缚缤剿赘熬赫蔫摹蔓蔗蔼熙蔚兢榛榕酵碟碴碱碳辕辖雌墅嘁踊蝉嘀幔镀舔熏箍箕箫舆僧孵瘩瘟彰粹漱" +
                           "漩漾慷寡寥谭褐褪隧嫡缨撵撩撮撬擒墩撰鞍蕊蕴樊樟橄敷豌醇磕磅碾憋嘶嘲嘹蝠蝎蝌蝗蝙嘿幢镊镐稽篓膘" +
                           "鲤鲫褒瘪瘤瘫凛澎潭潦澳潘澈澜澄憔懊憎翩褥谴鹤憨履嬉豫缭撼擂擅蕾薛薇擎翰噩橱橙瓢蟥霍霎辙冀踱蹂" +
                           "蟆螃螟噪鹦黔穆篡篷篙篱儒膳鲸瘾瘸糙燎濒憾懈窿缰壕藐檬檐檩檀礁磷瞭瞬瞳瞪曙蹋蟋蟀嚎赡镣魏簇儡徽" +
                           "爵朦臊鳄糜癌懦豁臀藕藤瞻嚣鳍癞瀑襟璧戳攒孽蘑藻鳖蹭蹬簸簿蟹靡癣羹鬓攘蠕巍鳞糯譬霹躏髓蘸镶瓤矗";

            return string.Format("{0}{1}{2}", _crabofirstName[@this.Next(_crabofirstName.Length - 1)], _lastName.Substring(@this.Next(0, _lastName.Length - 1), 1), _lastName.Substring(@this.Next(0, _lastName.Length - 1), 1));
        }
        #endregion
        #region 生成随机英文名字:NextEnglishName
        /// <summary>
        /// 随机生成英文名字
        /// </summary>
        /// <returns></returns>
        public static string NextEnglishName(this Random @this)
        {
            string name = string.Empty;
            string[] currentConsonant;
            string[] vowels = "a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,i,i,i,o,o,o,u,y,ee,ee,ea,ea,ey,eau,eigh,oa,oo,ou,ough,ay".Split(',');
            string[] commonConsonants = "s,s,s,s,t,t,t,t,t,n,n,r,l,d,sm,sl,sh,sh,th,th,th".Split(',');
            string[] averageConsonants = "sh,sh,st,st,b,c,f,g,h,k,l,m,p,p,ph,wh".Split(',');
            string[] middleConsonants = "x,ss,ss,ch,ch,ck,ck,dd,kn,rt,gh,mm,nd,nd,nn,pp,ps,tt,ff,rr,rk,mp,ll".Split(',');
            string[] rareConsonants = "j,j,j,v,v,w,w,w,z,qu,qu".Split(',');
            int[] lengthArray = new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3, 4 };
            int length = lengthArray[@this.Next(lengthArray.Length)];
            for (int i = 0; i < length; i++)
            {
                int letterType = @this.Next(1000);
                if (letterType < 775) currentConsonant = commonConsonants;
                else if (letterType < 875 && i > 0) currentConsonant = middleConsonants;
                else if (letterType < 985) currentConsonant = averageConsonants;
                else currentConsonant = rareConsonants;
                name += currentConsonant[@this.Next(currentConsonant.Length)];
                name += vowels[@this.Next(vowels.Length)];
                if (name.Length > 4 && @this.Next(1000) < 800) break;
                if (name.Length > 6 && @this.Next(1000) < 950) break;
                if (name.Length > 7) break;
            }
            int endingType = @this.Next(1000);
            if (name.Length > 6)
                endingType -= (name.Length * 25);
            else
                endingType += (name.Length * 10);
            if (endingType < 400) { }
            else if (endingType < 775) name += commonConsonants[@this.Next(commonConsonants.Length)];
            else if (endingType < 825) name += averageConsonants[@this.Next(averageConsonants.Length)];
            else if (endingType < 840) name += "ski";
            else if (endingType < 860) name += "son";
            else if (Regex.IsMatch(name, "(.+)(ay|e|ee|ea|oo)$") || name.Length < 5)
            {
                name = "Mc" + name.Substring(0, 1).ToUpper() + name.Substring(1);
                return name;
            }
            else name += "ez";
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);
            return name;
        }
        #endregion
        //1.随机密码，可能需要正则表达式限制，如： 
        //var password = random.NextString(@"\d{6,8}");
        //var passwordSalt = random.NextString(@"[0-9A-F]{16}");

        //随机国家、随机省、城市、地区名称，可能需要提供候选值，如： 
        //var country = random.NextCity("北京", "上海", "广州");

        //随机颜色（Color）：RGB 
        //随机字体（Font） 
        //随机位置（Point） 
        //随机旋转角度

    }
}
