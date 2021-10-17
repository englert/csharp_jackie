//﻿year	races	wins	podiums	poles	fastests
//1973	18	6	9	4	1

using System;                     // Console
using System.IO;                  // StreamReader
using System.Text;                // Encoding
using System.Collections.Generic; // List<>, Dictionary<>
using System.Linq;                // from where select

class Jackie{
    public int year;
    public int races;
    public int wins;
    public int podiums;
    public int poles;
    public int fastests;
}
class Program {
  public static void Main (string[] args) {
    var lista = new List<Jackie>();
    var f     = new StreamReader("jackie.txt");
    var elsosor = f.ReadLine();
    while(!f.EndOfStream){
        var sor = f.ReadLine().Split();
        var data = new Jackie();
        data.year     = int.Parse(sor[0]); // verseny éve
        data.races    = int.Parse(sor[1]); // versenyek száma az évben
        data.wins     = int.Parse(sor[2]); // megnyert versenyek
        data.podiums  = int.Parse(sor[3]); //dobogós eredmények
        data.poles    = int.Parse(sor[4]); //első helyről indulások száma
        data.fastests = int.Parse(sor[5]); //a leggyorsabb körök száma
        lista.Add(data);
    }
    // 3. feladat: {lista.Count}
    Console.WriteLine($"3. feladat: {lista.Count}");

    // 4. feladat: melyik évben indult a legtöbb versenyen?
    var year = ( from sor in lista  orderby sor.races select sor.year ).Last();
    Console.WriteLine($"4. feladat: {year}");

    // 5. feladat: évtizedenkénti sikerek
    var ev_siker = (from sor in lista select (sor.year, sor.wins) );
    var statisztika = new Dictionary<int, int>();
    foreach( var sor in ev_siker ){
        var evtized = sor.Item1 % 100 / 10 * 10;
        var siker   = sor.Item2;
        if (statisztika.ContainsKey(evtized)) {      // ha az evtized mint kulcs benne van a statisztika szótárban
                statisztika[evtized] += siker;                   // akkor a kulcshoz tartozó értékhez hozzáadja a siker-t
        } 
        else {                                   // egyébként
            statisztika[evtized] = siker;                     // a statisztika szótárban létrehozza az evtized változó alapján az új kulcsot a siker értékkel
        } 
    }
    Console.WriteLine(    $"5. feladat: ");
    foreach(var kulcs_ertek in statisztika){         // a foreach végiggyalogol a statisztika-n, a kulcs - érték párok a kulcs_ertek- be kerülnek
        Console.WriteLine($"        {kulcs_ertek.Key}-es évek {kulcs_ertek.Value} megnyert verseny");
        }

    // 6. feladat: jackie.html
    var fw = new StreamWriter("jackie.html");
    
    fw.WriteLine("<!DOCTYPE html>"); 
    fw.WriteLine("<html>");
    fw.WriteLine("   <head>");
    fw.WriteLine("       <style>td { border:1px solid black;}</style>");
    fw.WriteLine("   </head>");
    fw.WriteLine("   <body>");
    fw.WriteLine("       <h1>Jackie Stewart</h1>");
    fw.WriteLine("       <table>");
    foreach(var sor in lista){
        fw.WriteLine($"            <tr><td>{sor.year}</td><td>{sor.races}</td><td>{sor.wins}</td></tr>");
    }
    fw.WriteLine("       </table>");
    fw.WriteLine("     </body>");
    fw.WriteLine("</html>");
    
    fw.Close();
    //---------------------------------
  }
}