var OxO6504=["divpreview","idSource","Width","Height","TargetUrl","chk_Transparency","chk_AllowFullScreen","value","innerHTML","","$5","\x26","checked","wmode=\x22transparent\x22","allowfullscreen=\x22true\x22","\x3Cembed src=\x22","\x22 width=\x22","\x22 height=\x22","\x22 "," "," type=\x22application/x-shockwave-flash\x22 pluginspage=\x22http://www.macromedia.com/go/getflashplayer\x22 \x3E\x3C/embed\x3E\x0A","\x3Cobject xcodebase=","\x22http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab\x22"," height=\x22","\x22 classid=","\x22clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\x22 \x3E"," \x3Cparam name=\x22Movie\x22 value=\x22","\x22 /\x3E","\x3Cparam name=\x22wmode\x22 value=\x22transparent\x22/\x3E","\x3Cparam name=\x22allowFullScreen\x22 value=\x22true\x22/\x3E","\x3C/object\x3E"];var divpreview=Window_GetElement(window,OxO6504[0],true);var idSource=Window_GetElement(window,OxO6504[1],true);var Width=Window_GetElement(window,OxO6504[2],true);var Height=Window_GetElement(window,OxO6504[3],true);var TargetUrl=Window_GetElement(window,OxO6504[4],true);var chk_Transparency=Window_GetElement(window,OxO6504[5],true);var chk_AllowFullScreen=Window_GetElement(window,OxO6504[6],true);var editor=Window_GetDialogArguments(window);function do_preview(){var Ox119=GetEmbed();if(Ox119){if(idSource[OxO6504[7]]!=Ox119&&idSource[OxO6504[7]]!=null){idSource[OxO6504[7]]=Ox119;} ;divpreview[OxO6504[8]]=Ox119;} ;} ;function do_insert(){var Ox119=GetEmbed();if(Ox119){editor.PasteHTML(Ox119);} ;Window_CloseDialog(window);} ;function do_Close(){Window_CloseDialog(window);} ;function GetEmbed(){var Ox645=OxO6504[9];if(idSource[OxO6504[7]]!=OxO6504[9]&&idSource[OxO6504[7]]!=null){Ox645=idSource[OxO6504[7]];var Ox646=/(<object[^\>]*>[\s|\S]*?)(<embed[^\>]*?)(\ssrc=\s*)\s*("|')(.+?)\4([^>]*)(.*<\/embed>)[\s|\S]*?<\/object>/gi;if(Ox645.match(Ox646)){Ox645=Ox645.replace(Ox646,OxO6504[10]);} ;if(Ox645.indexOf(OxO6504[11])!=-1){TargetUrl[OxO6504[7]]=Ox645.substring(0,Ox645.indexOf(OxO6504[11]));} ;} else {return ;} ;var Ox647=OxO6504[9];var Oxda,Ox2b,Ox3da,Ox3db;Oxda=Width[OxO6504[7]]+OxO6504[9];Ox2b=Height[OxO6504[7]]+OxO6504[9];Ox3da=chk_Transparency[OxO6504[7]];if(Ox645==OxO6504[9]){divpreview[OxO6504[8]]=OxO6504[9];return ;} ;var Ox3de,Ox3e0;Ox3e0=OxO6504[9];Ox3de=chk_Transparency[OxO6504[12]]?OxO6504[13]:OxO6504[9];Ox3e0=chk_AllowFullScreen[OxO6504[12]]?OxO6504[14]:OxO6504[9];var Ox3e6=OxO6504[15]+Ox645+OxO6504[16]+Oxda+OxO6504[17]+Ox2b+OxO6504[18]+Ox3e0+OxO6504[19]+Ox3de+OxO6504[20];var Ox3e7=OxO6504[21]+OxO6504[22]+OxO6504[23]+Ox2b+OxO6504[16]+Oxda+OxO6504[24]+OxO6504[25]+OxO6504[26]+Ox645+OxO6504[27];if(chk_Transparency[OxO6504[12]]){Ox3e7=Ox3e7+OxO6504[28];} ;if(chk_AllowFullScreen[OxO6504[12]]){Ox3e7=Ox3e7+OxO6504[29];} ;Ox3e7=Ox3e7+Ox3e6+OxO6504[30];return Ox3e7;} ;