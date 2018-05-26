using System;
using System.Xml;
namespace TsuroTheSecond
{
    public class ToXml
    {
        // constant tags

        // colors
        //readonly string blue = "<color>blue</color>";
        //readonly string red = "<color>red</color>";
        //readonly string green = "<color>green</color>";
        //readonly string orange = "<color>orange</color>";
        //readonly string sienna = "<color>sienna</color>";
        //readonly string hotpink = "<color>hotpink</color>";
        //readonly string darkgreen = "<color>darkgreen</color>";
        //readonly string purple = "<color>purple</color>";

        //readonly string voidtag = "<void></void>";

        //readonly string horizontal = "<h></h>";
        //readonly string vertical = "<v></v>";

        //readonly string falsetag = "<false></false>";



        //public ToXml()
        //{
        //}

        //public string NametoXml(string name)
        //{
        //    string xmlname = "<player-name>{0}</player-name", name;
        //    return xmlname;
        //}

        //public string TiletoXml(Tile t)
        //{
        //    int start1 = t.paths[0][0];
        //    int end1 = t.paths[0][1];
        //    int start2 = t.paths[1][0];
        //    int end2 = t.paths[1][1];
        //    int start3 = t.paths[2][0];
        //    int end3 = t.paths[2][1];
        //    int start4 = t.paths[3][0];
        //    int end4 = t.paths[3][1];

        //    string tilestring = "<tile><connect><n>{0}</n><n>{1}</n></connect><connect><n>{2}</n><n>{3}</n></connect>" +
        //        "<connect><n>{4}</n><n>{5}</n></connect><connect><n>{6}</n><n>{7}</n></connect></tile>", start1, end1, start2, end2, start3, end3, start4, end4;
        //    return tilestring;
        //}

        ////public string BoardtoXml(Board b) { }

        //public string LocationtoXml(int x, int y, int p)
        //{
        //    string orientation;
        //    string line;
        //    string path;
        //    if (p == 0 || p == 1)
        //    {
        //        orientation = horizontal;
        //        line = y.ToString();
        //        if (p == 0) { path = (x * 2).ToString(); }
        //        else { path = ((x * 2) + 1).ToString(); }
        //    }
        //    else if (p == 4 || p == 5)
        //    {
        //        orientation = horizontal;
        //        line = (y + 1).ToString();
        //        if (p == 5) { path = (x * 2).ToString(); }
        //        else { path = ((x * 2) + 1).ToString(); }
        //    }
        //    else if(p == 2 || p == 3)
        //    {
        //        orientation = vertical;
        //        line = x.ToString();
        //        if (p == 2) { path = (y * 2).ToString(); }
        //        else { path = ((y * 2) + 1).ToString(); }
        //    }
        //    else if (p == 6 || p == 7)
        //    {
        //        orientation = vertical;
        //        line = (x + 1).ToString();
        //        if (p == 7) { path = (y * 2).ToString(); }
        //        else { path = ((y * 2) + 1).ToString(); }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("This is not a valid path position");
        //    }

        //    string ret =  "<pawn-loc>{0}<n>{1}</n><n>{2}</n></pawn-loc>", orientation, line, path;
        //    return ret;
        //}

    }
}
