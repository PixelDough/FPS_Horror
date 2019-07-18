using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomName : MonoBehaviour
{

    List<string> namesFirst = new List<string>();
    List<string> namesLast = new List<string>();

    public string myName;

    public TMPro.TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        // Male (25)
        namesFirst.Add("Michael");
        namesFirst.Add("Christopher");
        namesFirst.Add("Matthew");
        namesFirst.Add("Joshua");
        namesFirst.Add("David");
        namesFirst.Add("James");
        namesFirst.Add("Daniel");
        namesFirst.Add("Robert");
        namesFirst.Add("John");
        namesFirst.Add("Joseph");
        namesFirst.Add("Jason");
        namesFirst.Add("Justin");
        namesFirst.Add("Andrew");
        namesFirst.Add("Ryan");
        namesFirst.Add("William");
        namesFirst.Add("Brian");
        namesFirst.Add("Brandon");
        namesFirst.Add("Jonathan");
        namesFirst.Add("Nicholas");
        namesFirst.Add("Anthony");
        namesFirst.Add("Eric");
        namesFirst.Add("Aaron");
        namesFirst.Add("Kevin");
        namesFirst.Add("Thomas");
        namesFirst.Add("Steven");

        // Female (25)
        namesFirst.Add("Jessica");
        namesFirst.Add("Jennifer");
        namesFirst.Add("Amanda");
        namesFirst.Add("Ashley");
        namesFirst.Add("Sarah");
        namesFirst.Add("Stephanie");
        namesFirst.Add("Melissa");
        namesFirst.Add("Nicole");
        namesFirst.Add("Elizabeth");
        namesFirst.Add("Heather");
        namesFirst.Add("Tiffany");
        namesFirst.Add("Michelle");
        namesFirst.Add("Amber");
        namesFirst.Add("Megan");
        namesFirst.Add("Amy");
        namesFirst.Add("Rachel");
        namesFirst.Add("Kimberly");
        namesFirst.Add("Christina");
        namesFirst.Add("Lauren");
        namesFirst.Add("Crystal");
        namesFirst.Add("Brittany");
        namesFirst.Add("Samantha");
        namesFirst.Add("Laura");
        namesFirst.Add("Danielle");
        namesFirst.Add("Emily");

        // Last Names (75)
        namesLast.Add("SMITH");
        namesLast.Add("JOHNSON");
        namesLast.Add("WILLIAMS");
        namesLast.Add("JONES");
        namesLast.Add("BROWN");
        namesLast.Add("DAVIS");
        namesLast.Add("MILLER");
        namesLast.Add("WILSON");
        namesLast.Add("MOORE");
        namesLast.Add("TAYLOR");
        namesLast.Add("ANDERSON");
        namesLast.Add("THOMAS");
        namesLast.Add("JACKSON");
        namesLast.Add("WHITE");
        namesLast.Add("HARRIS");
        namesLast.Add("MARTIN");
        namesLast.Add("THOMPSON");
        namesLast.Add("GARCIA");
        namesLast.Add("MARTINEZ");
        namesLast.Add("ROBINSON");
        namesLast.Add("CLARK");
        namesLast.Add("RODRIGUEZ");
        namesLast.Add("LEWIS");
        namesLast.Add("LEE");
        namesLast.Add("WALKER");
        namesLast.Add("HALL");
        namesLast.Add("ALLEN");
        namesLast.Add("YOUNG");
        namesLast.Add("HERNANDEZ");
        namesLast.Add("KING");
        namesLast.Add("WRIGHT");
        namesLast.Add("LOPEZ");
        namesLast.Add("HILL");
        namesLast.Add("SCOTT");
        namesLast.Add("GREEN");
        namesLast.Add("ADAMS");
        namesLast.Add("BAKER");
        namesLast.Add("GONZALEZ");
        namesLast.Add("NELSON");
        namesLast.Add("CARTER");
        namesLast.Add("MITCHELL");
        namesLast.Add("PEREZ");
        namesLast.Add("ROBERTS");
        namesLast.Add("TURNER");
        namesLast.Add("PHILLIPS");
        namesLast.Add("CAMPBELL");
        namesLast.Add("PARKER");
        namesLast.Add("EVANS");
        namesLast.Add("EDWARDS");
        namesLast.Add("COLLINS");
        namesLast.Add("STEWART");
        namesLast.Add("SANCHEZ");
        namesLast.Add("MORRIS");
        namesLast.Add("ROGERS");
        namesLast.Add("REED");
        namesLast.Add("COOK");
        namesLast.Add("MORGAN");
        namesLast.Add("BELL");
        namesLast.Add("MURPHY");
        namesLast.Add("BAILEY");
        namesLast.Add("RIVERA");
        namesLast.Add("COOPER");
        namesLast.Add("RICHARDSON");
        namesLast.Add("COX");
        namesLast.Add("HOWARD");
        namesLast.Add("WARD");
        namesLast.Add("TORRES");
        namesLast.Add("PETERSON");
        namesLast.Add("GRAY");
        namesLast.Add("RAMIREZ");
        namesLast.Add("JAMES");
        namesLast.Add("WATSON");
        namesLast.Add("BROOKS");
        namesLast.Add("KELLY");
        namesLast.Add("SANDERS");

        Random.InitState(Random.Range(0, 9999999));

        myName = namesFirst[Random.Range(0, namesFirst.Count)] + " " + namesLast[Random.Range(0, namesLast.Count)];

        text.SetText(myName);
    }

}
