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

    public int evtized { get; private set; }

    public Jackie(string sor){var s = sor.Split();
        year    = int.Parse(s[0]);  // verseny éve
        races   = int.Parse(s[1]);  // versenyek száma az évben
        wins    = int.Parse(s[2]);  // megnyert versenyek
        podiums = int.Parse(s[3]);  // dobogós eredmények
        poles   = int.Parse(s[4]);  // első helyről indulások száma
        fastests = int.Parse(s[5]); // a leggyorsabb körök száma
        evtized = int.Parse( s[0].Substring(2, 1) ) * 10;
    }
}
class Program {
  public static void Main (string[] args) {
    var lista = new List<Jackie>();
    var f     = new StreamReader("jackie.txt");
    var elsosor = f.ReadLine();

    while (!f.EndOfStream){
            lista.Add(new Jackie(f.ReadLine()));
    }
    f.Close();
        
    // 3. feladat: {lista.Count}
    Console.WriteLine($"3. feladat: {lista.Count}");

    // 4. feladat: melyik évben indult a legtöbb versenyen?
    var year = ( from sor in lista  orderby sor.races select sor.year ).Last();
    Console.WriteLine($"4. feladat: {year}");

    // 5. feladat: évtizedenkénti sikerek
    var statisztika = new Dictionary<int, int>();
    foreach (var sor in lista){
            if (statisztika.ContainsKey(sor.evtized)){ 
                statisztika[sor.evtized] += sor.wins; 
            }
            else{   
                statisztika[sor.evtized] = sor.wins;
            }
    }
    
    Console.WriteLine(    $"5. feladat: ");
    foreach(var kulcs_ertek in statisztika){ 
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