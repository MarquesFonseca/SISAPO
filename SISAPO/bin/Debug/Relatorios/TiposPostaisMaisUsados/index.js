am4core.useTheme(am4themes_animated);

// create chart
var chart = am4core.create("chartdiv", am4charts.TreeMap);

/*
SELECT 
TiposPostais.Sigla, 
COUNT(TiposPostais.Sigla) AS Qtd
FROM TabelaObjetosSROLocal INNER JOIN TiposPostais 
ON Mid(TabelaObjetosSROLocal.CodigoObjeto, 1, 2) = TiposPostais.Sigla 
WHERE TiposPostais.TipoClassificacao = 'PAC'
GROUP BY TiposPostais.Sigla
ORDER BY 2
*/

//PC	6
chart.data = [{
	name: "PAC",
	children: 
	[
		{ name: "PC", value: 6 },
		{ name: "PB", value: 18 },
		{ name: "PN", value: 24 },
		{ name: "QC", value: 34 },
		{ name: "PG", value: 49 },
		{ name: "PE", value: 56 },
		{ name: "PO", value: 60 },
		{ name: "PQ", value: 88 },
		{ name: "PL", value: 120 },
		{ name: "PP", value: 212 },
		{ name: "PV", value: 267 },
		{ name: "PT", value: 426 },
		{ name: "QF", value: 619 },
		{ name: "QE", value: 628 },
		{ name: "QD", value: 669 },
		{ name: "PY", value: 833 },
		{ name: "PZ", value: 858 },
		{ name: "OL", value: 889 },
		{ name: "PW", value: 1131 },
		{ name: "PS", value: 1256 },
		{ name: "PU", value: 1581 },
		{ name: "PX", value: 1628 },
		{ name: "PM", value: 2298 }
	]
},
{
	name: "SEDEX",
	children: 
	[
		// { name: "DN", value: 1},
		// { name: "SM", value: 1},
		// { name: "DG", value: 1},
		// { name: "OB", value: 1},
		// { name: "DP", value: 1},
		{ name: "SI", value: 3},
		{ name: "DC", value: 3},
		{ name: "SL", value: 4},
		{ name: "SD", value: 4},
		{ name: "SB", value: 4},
		{ name: "SN", value: 5},
		{ name: "SA", value: 5},
		{ name: "DE", value: 8},
		{ name: "DO", value: 8},
		{ name: "IX", value: 9},
		{ name: "DW", value: 14},
		{ name: "SC", value: 15},
		{ name: "BC", value: 16},
		{ name: "DA", value: 16},
		{ name: "DV", value: 22},
		{ name: "SF", value: 25},
		{ name: "DJ", value: 27},
		{ name: "DR", value: 29},
		{ name: "OP", value: 40},
		{ name: "BF", value: 64},
		{ name: "SU", value: 79},
		{ name: "OO", value: 89},
		{ name: "EQ", value: 102},
		{ name: "DZ", value: 123},
		{ name: "DM", value: 145},
		{ name: "BJ", value: 145},
		{ name: "DQ", value: 148},
		{ name: "ON", value: 155},
		{ name: "OM", value: 190},
		{ name: "OF", value: 208},
		{ name: "OJ", value: 290},
		{ name: "OK", value: 351},
		{ name: "OI", value: 387},
		{ name: "OA", value: 458},
		{ name: "QB", value: 501},
		{ name: "OH", value: 504},
		{ name: "OG", value: 641},
		{ name: "DY", value: 695},
		{ name: "OD", value: 1764}
	]
},
{
	name: "DIVERSOS",
	children: 
	[
		// { name: "EA", value: 1   },
		// { name: "RD", value: 1   },
		// { name: "AR", value: 1   },
		// { name: "LG", value: 1   },
		// { name: "JY", value: 1   },
		// { name: "JK", value: 1   },
		// { name: "FG", value: 1   },
		// { name: "ET", value: 1   },
		// { name: "RJ", value: 1   },
		// { name: "RZ", value: 1   },
		// { name: "LI", value: 1   },
		// { name: "CX", value: 1   },
		// { name: "CW", value: 1   },
		// { name: "CR", value: 1   },
		// { name: "CQ", value: 1   },
		// { name: "CG", value: 1   },
		// { name: "CF", value: 1   },
		// { name: "CE", value: 1   },
		// { name: "CB", value: 1   },
		// { name: "UV", value: 1   },
		// { name: "CA", value: 1   },
		// { name: "EH", value: 1   },
		{ name: "LH", value: 2   },
		{ name: "LA", value: 2   },
		{ name: "MA", value: 2   },
		{ name: "UX", value: 2   },
		{ name: "UQ", value: 2   },
		{ name: "US", value: 2   },
		{ name: "UL", value: 2   },
		{ name: "BY", value: 3   },
		{ name: "LK", value: 3   },
		{ name: "IA", value: 3   },
		{ name: "EX", value: 3   },
		{ name: "JJ", value: 3   },
		{ name: "UE", value: 3   },
		{ name: "RO", value: 3   },
		{ name: "UM", value: 3   },
		{ name: "UJ", value: 3   },
		{ name: "LJ", value: 4   },
		{ name: "IU", value: 4   },
		{ name: "EE", value: 4   },
		{ name: "UA", value: 4   },
		{ name: "UF", value: 4   },
		{ name: "MM", value: 4   },
		{ name: "UT", value: 4   },
		{ name: "CC", value: 4   },
		{ name: "EB", value: 5   },
		{ name: "UG", value: 5   },
		{ name: "CH", value: 5   },
		{ name: "MB", value: 5   },
		{ name: "RS", value: 5   },
		{ name: "RT", value: 5   },
		{ name: "UY", value: 5   },
		{ name: "RP", value: 6   },
		{ name: "EV", value: 6   },
		{ name: "UB", value: 6   },
		{ name: "RK", value: 6   },
		{ name: "MT", value: 6   },
		{ name: "CY", value: 6   },
		{ name: "RQ", value: 6   },
		{ name: "BZ", value: 7   },
		{ name: "RN", value: 7   },
		{ name: "UH", value: 7   },
		{ name: "CP", value: 7   },
		{ name: "RY", value: 7   },
		{ name: "JG", value: 8   },
		{ name: "JS", value: 8   },
		{ name: "RM", value: 8   },
		{ name: "RC", value: 9   },
		{ name: "RA", value: 9   },
		{ name: "LT", value: 9   },
		{ name: "RW", value: 9   },
		{ name: "RG", value: 10  },
		{ name: "ME", value: 11  },
		{ name: "RF", value: 12  },
		{ name: "UD", value: 13  },
		{ name: "RL", value: 13  },
		{ name: "YA", value: 13  },
		{ name: "LW", value: 14  },
		{ name: "JH", value: 15  },
		{ name: "RR", value: 17  },
		{ name: "CJ", value: 21  },
		{ name: "JP", value: 22  },
		{ name: "RH", value: 22  },
		{ name: "BV", value: 24  },
		{ name: "LP", value: 26  },
		{ name: "JO", value: 27  },
		{ name: "BP", value: 30  },
		{ name: "RB", value: 30  },
		{ name: "RV", value: 31  },
		{ name: "RX", value: 40  },
		{ name: "JC", value: 44  },
		{ name: "RU", value: 44  },
		{ name: "FF", value: 53  },
		{ name: "LY", value: 70  },
		{ name: "BT", value: 86  },
		{ name: "JR", value: 87  },
		{ name: "FB", value: 90  },
		{ name: "BG", value: 92  },
		{ name: "MH", value: 93  },
		{ name: "BK", value: 93  },
		{ name: "RE", value: 103 },
		{ name: "LM", value: 105 },
		{ name: "BO", value: 125 },
		{ name: "JB", value: 125 },
		{ name: "BH", value: 142 },
		{ name: "BI", value: 146 },
		{ name: "BR", value: 185 },
		{ name: "LZ", value: 187 },
		{ name: "LE", value: 192 },
		{ name: "BL", value: 264 },
		{ name: "LO", value: 286 },
		{ name: "LL", value: 361 },
		{ name: "LS", value: 421 },
		{ name: "NX", value: 528 },
		{ name: "LX", value: 539 },
		{ name: "BE", value: 631 },
		{ name: "JT", value: 677 },
		{ name: "JU", value: 1194},
		{ name: "JN", value: 1218},
		{ name: "LB", value: 1917}
	]
}];

chart.colors.step = 2;

// define data fields
chart.dataFields.value = "value";
chart.dataFields.name = "name";
chart.dataFields.children = "children";

chart.zoomable = false;

// level 0 series template
var level0SeriesTemplate = chart.seriesTemplates.create("0");
var level0ColumnTemplate = level0SeriesTemplate.columns.template;

level0ColumnTemplate.column.cornerRadius(10, 10, 10, 10);
level0ColumnTemplate.fillOpacity = 0;
level0ColumnTemplate.strokeWidth = 4;
level0ColumnTemplate.strokeOpacity = 0;

// level 1 series template
var level1SeriesTemplate = chart.seriesTemplates.create("1");
var level1ColumnTemplate = level1SeriesTemplate.columns.template;

level1SeriesTemplate.tooltip.animationDuration = 0;
level1SeriesTemplate.strokeOpacity = 1;

level1ColumnTemplate.column.cornerRadius(10, 10, 10, 10)
level1ColumnTemplate.fillOpacity = 1;
level1ColumnTemplate.strokeWidth = 4;
level1ColumnTemplate.stroke = am4core.color("#ffffff");

var bullet1 = level1SeriesTemplate.bullets.push(new am4charts.LabelBullet());
bullet1.locationY = 0.5;
bullet1.locationX = 0.5;
bullet1.label.text = "{name}";
bullet1.label.fill = am4core.color("#ffffff");

chart.maxLevels = 2;