var OxO44bc=["inp_width","inp_height","sel_align","sel_valign","inp_bgColor","inp_borderColor","inp_borderColorLight","inp_borderColorDark","inp_class","inp_id","inp_tooltip","value","bgColor","backgroundColor","style","id","borderColor","borderColorLight","borderColorDark","className","width","height","align","vAlign","title","[[ValidNumber]]","[[ValidID]]","","class","valign","onclick"];var inp_width=Window_GetElement(window,OxO44bc[0],true);var inp_height=Window_GetElement(window,OxO44bc[1],true);var sel_align=Window_GetElement(window,OxO44bc[2],true);var sel_valign=Window_GetElement(window,OxO44bc[3],true);var inp_bgColor=Window_GetElement(window,OxO44bc[4],true);var inp_borderColor=Window_GetElement(window,OxO44bc[5],true);var inp_borderColorLight=Window_GetElement(window,OxO44bc[6],true);var inp_borderColorDark=Window_GetElement(window,OxO44bc[7],true);var inp_class=Window_GetElement(window,OxO44bc[8],true);var inp_id=Window_GetElement(window,OxO44bc[9],true);var inp_tooltip=Window_GetElement(window,OxO44bc[10],true);SyncToView=function SyncToView_Tr(){inp_bgColor[OxO44bc[11]]=element.getAttribute(OxO44bc[12])||element[OxO44bc[14]][OxO44bc[13]];inp_id[OxO44bc[11]]=element.getAttribute(OxO44bc[15]);inp_bgColor[OxO44bc[14]][OxO44bc[13]]=inp_bgColor[OxO44bc[11]];inp_borderColor[OxO44bc[11]]=element.getAttribute(OxO44bc[16]);inp_borderColor[OxO44bc[14]][OxO44bc[13]]=inp_borderColor[OxO44bc[11]];inp_borderColorLight[OxO44bc[11]]=element.getAttribute(OxO44bc[17]);inp_borderColorLight[OxO44bc[14]][OxO44bc[13]]=inp_borderColorLight[OxO44bc[11]];inp_borderColorDark[OxO44bc[11]]=element.getAttribute(OxO44bc[18]);inp_borderColorDark[OxO44bc[14]][OxO44bc[13]]=inp_borderColorDark[OxO44bc[11]];inp_class[OxO44bc[11]]=element[OxO44bc[19]];inp_width[OxO44bc[11]]=element.getAttribute(OxO44bc[20])||element[OxO44bc[14]][OxO44bc[20]];inp_height[OxO44bc[11]]=element.getAttribute(OxO44bc[21])||element[OxO44bc[14]][OxO44bc[21]];sel_align[OxO44bc[11]]=element.getAttribute(OxO44bc[22]);sel_valign[OxO44bc[11]]=element.getAttribute(OxO44bc[23]);inp_tooltip[OxO44bc[11]]=element.getAttribute(OxO44bc[24]);} ;SyncTo=function SyncTo_Tr(element){if(inp_bgColor[OxO44bc[11]]){if(element[OxO44bc[14]][OxO44bc[13]]){element[OxO44bc[14]][OxO44bc[13]]=inp_bgColor[OxO44bc[11]];} else {element[OxO44bc[12]]=inp_bgColor[OxO44bc[11]];} ;} else {element.removeAttribute(OxO44bc[12]);} ;element[OxO44bc[16]]=inp_borderColor[OxO44bc[11]];element[OxO44bc[17]]=inp_borderColorLight[OxO44bc[11]];element[OxO44bc[18]]=inp_borderColorDark[OxO44bc[11]];element[OxO44bc[19]]=inp_class[OxO44bc[11]];if(element[OxO44bc[14]][OxO44bc[20]]||element[OxO44bc[14]][OxO44bc[21]]){try{element[OxO44bc[14]][OxO44bc[20]]=inp_width[OxO44bc[11]];element[OxO44bc[14]][OxO44bc[21]]=inp_height[OxO44bc[11]];} catch(er){alert(OxO44bc[25]);} ;} else {try{element[OxO44bc[20]]=inp_width[OxO44bc[11]];element[OxO44bc[21]]=inp_height[OxO44bc[11]];} catch(er){alert(OxO44bc[25]);} ;} ;var Ox36d=/[^a-z\d]/i;if(Ox36d.test(inp_id.value)){alert(OxO44bc[26]);return ;} ;element[OxO44bc[22]]=sel_align[OxO44bc[11]];element[OxO44bc[15]]=inp_id[OxO44bc[11]];element[OxO44bc[23]]=sel_valign[OxO44bc[11]];element[OxO44bc[24]]=inp_tooltip[OxO44bc[11]];if(element[OxO44bc[15]]==OxO44bc[27]){element.removeAttribute(OxO44bc[15]);} ;if(element[OxO44bc[12]]==OxO44bc[27]){element.removeAttribute(OxO44bc[12]);} ;if(element[OxO44bc[16]]==OxO44bc[27]){element.removeAttribute(OxO44bc[16]);} ;if(element[OxO44bc[17]]==OxO44bc[27]){element.removeAttribute(OxO44bc[17]);} ;if(element[OxO44bc[7]]==OxO44bc[27]){element.removeAttribute(OxO44bc[7]);} ;if(element[OxO44bc[19]]==OxO44bc[27]){element.removeAttribute(OxO44bc[19]);} ;if(element[OxO44bc[19]]==OxO44bc[27]){element.removeAttribute(OxO44bc[28]);} ;if(element[OxO44bc[22]]==OxO44bc[27]){element.removeAttribute(OxO44bc[22]);} ;if(element[OxO44bc[23]]==OxO44bc[27]){element.removeAttribute(OxO44bc[29]);} ;if(element[OxO44bc[24]]==OxO44bc[27]){element.removeAttribute(OxO44bc[24]);} ;if(element[OxO44bc[20]]==OxO44bc[27]){element.removeAttribute(OxO44bc[20]);} ;if(element[OxO44bc[21]]==OxO44bc[27]){element.removeAttribute(OxO44bc[21]);} ;} ;inp_borderColor[OxO44bc[30]]=function inp_borderColor_onclick(){SelectColor(inp_borderColor);} ;inp_bgColor[OxO44bc[30]]=function inp_bgColor_onclick(){SelectColor(inp_bgColor);} ;inp_borderColorLight[OxO44bc[30]]=function inp_borderColorLight_onclick(){SelectColor(inp_borderColorLight);} ;inp_borderColorDark[OxO44bc[30]]=function inp_borderColorDark_onclick(){SelectColor(inp_borderColorDark);} ;