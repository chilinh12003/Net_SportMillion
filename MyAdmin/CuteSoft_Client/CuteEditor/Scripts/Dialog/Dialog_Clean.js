var OxO59a6=["ig","\x3C/?[^\x3E]*\x3E","","allhtml","\x3C\x5C?xml[^\x3E]*\x3E","\x3C/?[a-z]+:[^\x3E]*\x3E","(\x3C[^\x3E]+) class=[^ |^\x3E]*([^\x3E]*\x3E)","$1 $2","(\x3C[^\x3E]+) style=\x22[^\x22]*\x22([^\x3E]*\x3E)","\x3Cspan[^\x3E]*\x3E\x3C/span[^\x3E]*\x3E","\x3Cspan\x3E\x3Cspan\x3E","\x3Cspan\x3E","\x3C/span\x3E\x3C/span\x3E","\x3C/span\x3E","[ ]*\x3E","\x3E","word","css","\x3C/?font[^\x3E]*\x3E","font","\x3C/?span[^\x3E]*\x3E","span"];var editor=Window_GetDialogArguments(window);function execRE(Ox28e,Ox28f,Oxc7){var Ox290= new RegExp(Ox28e,OxO59a6[0]);return Oxc7.replace(Ox290,Ox28f);} ;function getContent(){return editor.GetBodyInnerHTML();} ;function setContent(Oxc7){editor.SetHTML(Oxc7);} ;function codeCleaner(Ox20f){var Oxc7=getContent();switch(Ox20f){case OxO59a6[3]:Oxc7=execRE(OxO59a6[1],OxO59a6[2],Oxc7);break ;;case OxO59a6[16]:Oxc7=execRE(OxO59a6[4],OxO59a6[2],Oxc7);Oxc7=execRE(OxO59a6[5],OxO59a6[2],Oxc7);Oxc7=execRE(OxO59a6[6],OxO59a6[7],Oxc7);Oxc7=execRE(OxO59a6[8],OxO59a6[7],Oxc7);Oxc7=execRE(OxO59a6[9],OxO59a6[2],Oxc7);Oxc7=execRE(OxO59a6[10],OxO59a6[11],Oxc7);Oxc7=execRE(OxO59a6[12],OxO59a6[13],Oxc7);Oxc7=execRE(OxO59a6[14],OxO59a6[15],Oxc7);break ;;case OxO59a6[17]:Oxc7=execRE(OxO59a6[6],OxO59a6[7],Oxc7);Oxc7=execRE(OxO59a6[8],OxO59a6[7],Oxc7);break ;;case OxO59a6[19]:Oxc7=execRE(OxO59a6[18],OxO59a6[2],Oxc7);break ;;case OxO59a6[21]:Oxc7=execRE(OxO59a6[20],OxO59a6[2],Oxc7);break ;;} ;setContent(Oxc7);} ;