function checkFun() 
{
    var n1 = document.getElementsByClassName("table_lst table_lst_spn")[0].rows.length;
    
    var i=0,j=0;
    var str="";
     
    for(i=1; i<n1;i++)
    {
        var n2 = document.getElementsByClassName("table_lst table_lst_spn")[0].rows[i].cells.length;
        
        var name = document.getElementsByClassName("table_lst table_lst_spn")[0].rows[i].cells.item(1).innerHTML;
        var s = name.split("\n");

        str=str+s[2].substring(6).replace("&lt;", "<").replace("&gt;", ">")+"\n";
        
    }
    console.log(str);
    var tempInput = document.createElement("textarea");
    tempInput.value = str.substr(0, (str.length - 1));
    document.body.appendChild(tempInput);
    tempInput.select();
    document.execCommand("copy");
    document.body.removeChild(tempInput);
    var aTags = document.getElementsByTagName("a");
    var searchText = "NEXT";
    var found;

    for (var i = 0; i < aTags.length; i++) {
    if (aTags[i].textContent == searchText) {
        found = aTags[i];
        break;
    }
    }
    window.location.href = found.href;
}


function original() 
{
    var n1 = document.getElementsByClassName("table_lst table_lst_spn")[0].rows.length;
    
    var i=0,j=0;
    var str="";
     
    for(i=0; i<n1;i++)
    {
     
        var n2 = document.getElementsByClassName("table_lst table_lst_spn")[0].rows[i].cells.length;
     
        for(j=0; j<n2;j++)
        {
            var x=document.getElementsByClassName("table_lst table_lst_spn")[0].rows[i].cells.item(j).innerHTML;
            str=str+x+":";
        }
    str=str+"#";
       
    }
    console.log(str);
    
       
    }