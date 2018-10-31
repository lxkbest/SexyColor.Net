/**
 * Resize function without multiple trigger
 * 
 * Usage:
 * $(window).smartresize(function(){  
 *     // code here
 * });
 * 防抖（Debounce）和节流（throttle）都是用来控制某个函数在一定时间内执行多少次的技巧，两者相似而又不同。-----2017-3-6 伍冠源
 * http://www.css88.com/archives/7010
 */
(function($, sr) {
	// debouncing function from John Hann
	// http://unscriptable.com/index.php/2009/03/20/debouncing-javascript-methods/
	var debounce = function(func, threshold, execAsap) {
		var timeout;

		return function debounced() {
			var obj = this,
				args = arguments;

			function delayed() {
				if(!execAsap)
					func.apply(obj, args);
				timeout = null;
			}

			if(timeout)
				clearTimeout(timeout);
			else if(execAsap)
				func.apply(obj, args);

			timeout = setTimeout(delayed, threshold || 100);
		};
	};

	// smartresize 
	jQuery.fn[sr] = function(fn) { return fn ? this.bind('resize', debounce(fn)) : this.trigger(sr); };

})(jQuery, 'smartresize');
/**
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

//

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

var CURRENT_URL = window.location.href.split('#')[0].split('?')[0],
	$BODY = $('body'),
	$MENU_TOGGLE = $('#menu_toggle'),
	$SIDEBAR_MENU = $('#sidebar-menu'),
	$SIDEBAR_FOOTER = $('.sidebar-footer'),
	$LEFT_COL = $('.left_col'),
	$RIGHT_COL = $('.right_col'),
	$NAV_MENU = $('.nav_menu'),
    $FOOTER = $('footer');

var sidebarUrl = GetQueryString("sidebarUrl");

if (sidebarUrl != null && sidebarUrl.toString().length > 1) {
    CURRENT_URL = sidebarUrl;
}

// Sidebar
function init_sidebar() {
	// TODO: This is some kind of easy fix, maybe we can improve this
	var setContentHeight = function() {
		// reset height
		$RIGHT_COL.css('min-height', $(window).height());

		var bodyHeight = $BODY.outerHeight(),
			footerHeight = $BODY.hasClass('footer_fixed') ? -10 : $FOOTER.height(),
			leftColHeight = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(),
			contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

		// normalize content
		contentHeight -= $NAV_MENU.height() + footerHeight;

		$RIGHT_COL.css('min-height', contentHeight);
	};

	$SIDEBAR_MENU.find('a').on('click', function(ev) {
		console.log('clicked - sidebar_menu');
		var $li = $(this).parent();

		if($li.is('.active')) {
			$li.removeClass('active active-sm');
			$('ul:first', $li).slideUp(function() {
				setContentHeight();
			});
		} else {
			// prevent closing menu if we are on child menu
			if(!$li.parent().is('.child_menu')) {
				$SIDEBAR_MENU.find('li').removeClass('active active-sm');
				$SIDEBAR_MENU.find('li ul').slideUp();
			} else {
				if($BODY.is(".nav-sm")) {
					$SIDEBAR_MENU.find("li").removeClass("active active-sm");
					$SIDEBAR_MENU.find("li ul").slideUp();
				}
			}
			$li.addClass('active');

			$('ul:first', $li).slideDown(function() {
				setContentHeight();
			});
		}
	});

	// toggle small or large menu 
	$MENU_TOGGLE.on('click', function() {
		console.log('clicked - menu toggle');

		if($BODY.hasClass('nav-md')) {
			$SIDEBAR_MENU.find('li.active ul').hide();
			$SIDEBAR_MENU.find('li.active').addClass('active-sm').removeClass('active');
		} else {
			$SIDEBAR_MENU.find('li.active-sm ul').show();
			$SIDEBAR_MENU.find('li.active-sm').addClass('active').removeClass('active-sm');
		}

		$BODY.toggleClass('nav-md nav-sm');

		setContentHeight();
    });

    //2017-5-12 伍冠源修改


	// check active menu
	$SIDEBAR_MENU.find('a[href="' + CURRENT_URL + '"]').parent('li').addClass('current-page');

	$SIDEBAR_MENU.find('a').filter(function() {
		return this.href == CURRENT_URL;
	}).parent('li').addClass('current-page').parents('ul').slideDown(function() {
		setContentHeight();
	}).parent().addClass('active');

	// recompute content when resizing
	$(window).smartresize(function() {
		setContentHeight();
	});

	setContentHeight();

	// fixed sidebar
	if($.fn.mCustomScrollbar) {
		$('.menu_fixed').mCustomScrollbar({
			autoHideScrollbar: true,
			theme: 'minimal',
			mouseWheel: { preventDefault: true }
		});
	}
};

//随机数
var randNum = function() {
	return(Math.floor(Math.random() * (1 + 40 - 20))) + 20;
};

// Panel toolbox
$(document).ready(function() {
    $(document).on('click', '.collapse-link',function() {
		var $BOX_PANEL = $(this).closest('.x_panel'),
			$ICON = $(this).find('i'),
			$BOX_CONTENT = $BOX_PANEL.find('.x_content');

		// fix for some div with hardcoded fix class
		if($BOX_PANEL.attr('style')) {
			$BOX_CONTENT.slideToggle(200, function() {
				$BOX_PANEL.removeAttr('style');
			});
		} else {
			$BOX_CONTENT.slideToggle(200);
			$BOX_PANEL.css('height', 'auto');
		}

		$ICON.toggleClass('fa-chevron-up fa-chevron-down');
	});

    $(document).on('click', '.close-link',function() {
		var $BOX_PANEL = $(this).closest('.x_panel');

		$BOX_PANEL.remove();
	});
});
// /Panel toolbox

// Tooltip
$(document).ready(function() {
	$('[data-toggle="tooltip"]').tooltip({
		container: 'body'
	});
});
// /Tooltip

// Progressbar 进度条
if (typeof NProgress != 'undefined') {
    $(document).ready(function () {
        NProgress.start();
    });

    $(window).load(function () {
        NProgress.done();
    });
}
// /Progressbar

// Switchery IOS7样式开关
$(document).ready(function() {
	if($(".js-switch")[0]) {
		var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
		elems.forEach(function(html) {
			var switchery = new Switchery(html, {
				color: '#26B99A'
			});
		});
	}
});

// iCheck 单选、复选框插件
$(document).ready(function() {
	if($("input.flat")[0]) {
		$(document).ready(function() {
			$('input.flat').iCheck({
				checkboxClass: 'icheckbox_flat-green',
				radioClass: 'iradio_flat-green'
			});
		});
	}
});

//根据年月日获取毫秒数
function gd(year, month, day) {
	return new Date(year, month - 1, day).getTime();
}

function init_flot_chart() {

	if(typeof($.plot) === 'undefined') { return; }

	console.log('init_flot_chart');

	var arr_data1 = [
		[gd(2012, 1, 1), 17],
		[gd(2012, 1, 2), 74],
		[gd(2012, 1, 3), 6],
		[gd(2012, 1, 4), 39],
		[gd(2012, 1, 5), 20],
		[gd(2012, 1, 6), 85],
		[gd(2012, 1, 7), 7]
	];

	var arr_data2 = [
		[gd(2012, 1, 1), 82],
		[gd(2012, 1, 2), 23],
		[gd(2012, 1, 3), 66],
		[gd(2012, 1, 4), 9],
		[gd(2012, 1, 5), 119],
		[gd(2012, 1, 6), 6],
		[gd(2012, 1, 7), 9]
	];

	var arr_data3 = [
		[0, 1],
		[1, 9],
		[2, 6],
		[3, 10],
		[4, 5],
		[5, 17],
		[6, 6],
		[7, 10],
		[8, 7],
		[9, 11],
		[10, 35],
		[11, 9],
		[12, 12],
		[13, 5],
		[14, 3],
		[15, 4],
		[16, 9]
	];

	var chart_plot_01_settings = {
		series: {
			lines: {
				show: false,
				fill: true
			},
			splines: {
				show: true,
				tension: 0.4,
				lineWidth: 1,
				fill: 0.4
			},
			points: {
				radius: 0,
				show: true
			},
			shadowSize: 2
		},
		grid: {
			verticalLines: true,
			hoverable: true,
			clickable: true,
			tickColor: "#d5d5d5",
			borderWidth: 1,
			color: '#fff'
		},
		colors: ["rgba(38, 185, 154, 0.38)", "rgba(3, 88, 106, 0.38)"],
		xaxis: {
			tickColor: "rgba(51, 51, 51, 0.06)",
			mode: "time",
			tickSize: [1, "day"],
			//tickLength: 10,
			axisLabel: "Date",
			timeformat: "%b%e日",
			monthNames: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
			axisLabelUseCanvas: true,
			axisLabelFontSizePixels: 12,
			axisLabelFontFamily: 'Verdana, Arial',
			axisLabelPadding: 10
		},
		yaxis: {
			ticks: 8,
			tickColor: "rgba(51, 51, 51, 0.06)",
		},
		tooltip: false
	}

	if($("#chart_plot_01").length) {
		console.log('Plot1');

		$.plot($("#chart_plot_01"), [arr_data1, arr_data2], chart_plot_01_settings);
	}

}

// Sidebar
function init_sidebar() {
	// TODO: This is some kind of easy fix, maybe we can improve this
	var setContentHeight = function() {
		// reset height
		$RIGHT_COL.css('min-height', $(window).height());

		var bodyHeight = $BODY.outerHeight(),
			footerHeight = $BODY.hasClass('footer_fixed') ? -10 : $FOOTER.height(),
			leftColHeight = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(),
			contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

		// normalize content
		contentHeight -= $NAV_MENU.height() + footerHeight;

		$RIGHT_COL.css('min-height', contentHeight);
	};

	$SIDEBAR_MENU.find('a').on('click', function(ev) {
		console.log('clicked - sidebar_menu');
		var $li = $(this).parent();

		if($li.is('.active')) {
			$li.removeClass('active active-sm');
			$('ul:first', $li).slideUp(function() {
				setContentHeight();
			});
		} else {
			// prevent closing menu if we are on child menu
			if(!$li.parent().is('.child_menu')) {
				$SIDEBAR_MENU.find('li').removeClass('active active-sm');
				$SIDEBAR_MENU.find('li ul').slideUp();
			} else {
				if($BODY.is(".nav-sm")) {
					$SIDEBAR_MENU.find("li").removeClass("active active-sm");
					$SIDEBAR_MENU.find("li ul").slideUp();
				}
			}
			$li.addClass('active');

			$('ul:first', $li).slideDown(function() {
				setContentHeight();
			});
		}
	});

	// toggle small or large menu 
	$MENU_TOGGLE.on('click', function() {
		console.log('clicked - menu toggle');

		if($BODY.hasClass('nav-md')) {
			$SIDEBAR_MENU.find('li.active ul').hide();
			$SIDEBAR_MENU.find('li.active').addClass('active-sm').removeClass('active');
		} else {
			$SIDEBAR_MENU.find('li.active-sm ul').show();
			$SIDEBAR_MENU.find('li.active-sm').addClass('active').removeClass('active-sm');
		}

		$BODY.toggleClass('nav-md nav-sm');

		setContentHeight();
	});

	// check active menu
	$SIDEBAR_MENU.find('a[href="' + CURRENT_URL + '"]').parent('li').addClass('current-page');

	$SIDEBAR_MENU.find('a').filter(function() {
		return this.href == CURRENT_URL;
	}).parent('li').addClass('current-page').parents('ul').slideDown(function() {
		setContentHeight();
	}).parent().addClass('active');

	// recompute content when resizing
	$(window).smartresize(function() {
		setContentHeight();
	});

	setContentHeight();

	// fixed sidebar
	if($.fn.mCustomScrollbar) {
		$('.menu_fixed').mCustomScrollbar({
			autoHideScrollbar: true,
			theme: 'minimal',
			mouseWheel: { preventDefault: true }
		});
	}
};

/* INPUT MASK 格式化录入插件 如 1/5/2017*/
function init_InputMask() {

	if(typeof($.fn.inputmask) === 'undefined') { return; }
	console.log('init_InputMask');
	$(":input").inputmask();

};

/* PARSLEY  验证校验插件*/

function init_parsley() {

	if(typeof(parsley) === 'undefined') { return; }
	console.log('init_parsley');
	//验证之前触发
	$ /*.listen*/ ('parsley:field:validate', function() {
		validateFront();
	});
	$('#demo-form .btn').on('click', function() {
		$('#demo-form').parsley().validate();
		validateFront();
	});
	//我调试的时候找不到这个元素，不知道什么意思，先放着。
	var validateFront = function() {
		if(true === $('#demo-form').parsley().isValid()) {
			$('.bs-callout-info').removeClass('hidden');
			$('.bs-callout-warning').addClass('hidden');
		} else {
			$('.bs-callout-info').addClass('hidden');
			$('.bs-callout-warning').removeClass('hidden');
		}
	};

	$ /*.listen*/ ('parsley:field:validate', function() {
		validateFront();
	});
	$('#demo-form2 .btn').on('click', function() {
		$('#demo-form2').parsley().validate();
		validateFront();
	});
	//验证钱所做的操作
	var validateFront = function() {
		if($('#demo-form2').length==0){return;}
		if(true === $('#demo-form2').parsley().isValid()) {
			$('.bs-callout-info').removeClass('hidden');
			$('.bs-callout-warning').addClass('hidden');
		} else {
			$('.bs-callout-info').addClass('hidden');
			$('.bs-callout-warning').removeClass('hidden');
		}
	};

	try {
		hljs.initHighlightingOnLoad();
	} catch(err) {}

};

//  日期范围选择
/* DATE RANGE PICKER */

function init_daterangepicker() {

	if(typeof($.fn.daterangepicker) === 'undefined') { return; }
	console.log('init_date range picker');

	var cb = function(start, end, label) {
		console.log('date:' + start.toISOString(), end.toISOString(), label);

		$('#reportrange span').html(start.format('YYYY-MM-DD') + ' - ' + end.format('YYYY-MM-DD'));
	};

	var optionSet1 = {
		startDate: moment().subtract(29, 'days').format('L'),
		endDate: moment().format('L'),
		// minDate: ,
		// maxDate: ,
		minDate: "YYYY-MM-DD",
		maxDate: "YYYY-MM-DD",
		dateLimit: {
			days: 60
		},
		showDropdowns: true,
		showWeekNumbers: true,
		timePicker: false,
		timePickerIncrement: 1,
		timePicker12Hour: true,
		ranges: {
			'今天': [moment(), moment()],
			'昨天': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
			'最近7天': [moment().subtract(6, 'days'), moment()],
			'最近30天': [moment().subtract(29, 'days'), moment()],
			'这个月': [moment().startOf('month'), moment().endOf('month')],
			'上一个月': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
		},
		opens: 'left',
		buttonClasses: ['btn btn-default'],
		applyClass: 'btn-small btn-primary',
		cancelClass: 'btn-small',
		format: 'YYYY-MM-DD', //'MM/DD/YYYY'
		separator: ' 至 ',
		locale: {
			applyLabel: '确定',
			cancelLabel: '取消',
			fromLabel: '起始时间',
			toLabel: '结束时间',
			customRangeLabel: '选择',
			daysOfWeek: ['日', '一', '二', '三', '四', '五', '六'],
			monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
			firstDay: 1
		}
	};

	$('#reportrange span').html(moment().subtract(29, 'days').format('YYYY-MM-DD') + ' - ' + moment().format('YYYY-MM-DD'));
	$('#reportrange').daterangepicker(optionSet1, cb);
	$('#reportrange').on('show.daterangepicker', function() {
		console.log("show event fired");
	});
	$('#reportrange').on('hide.daterangepicker', function() {
		console.log("hide event fired");
	});
	$('#reportrange').on('apply.daterangepicker', function(ev, picker) {
		console.log("apply event fired, start/end dates are " + picker.startDate.format('YYYY-MM-DD') + " 至 " + picker.endDate.format('YYYY-MM-DD'));
	});
	$('#reportrange').on('cancel.daterangepicker', function(ev, picker) {
		console.log("cancel event fired");
	});
	$('#options1').click(function() {
		$('#reportrange').data('daterangepicker').setOptions(optionSet1, cb);
	});
	$('#options2').click(function() {
		$('#reportrange').data('daterangepicker').setOptions(optionSet2, cb);
	});
	$('#destroy').click(function() {
		$('#reportrange').data('daterangepicker').remove();
	});

}

//tags input 标签插件
function init_TagsInput() {

	if(typeof $.fn.tagsInput !== 'undefined') {

		$('#tags_1').tagsInput({
			width: 'auto'
		});

	}

};

/* COMPOSE  jquery开关切换*/
function init_compose() {

	if(typeof($.fn.slideToggle) === 'undefined') { return; }
	console.log('init_compose');

	$('#compose, .compose-close').click(function() {
		$('.compose').slideToggle();
	});

};
/*圆环图*/
function init_chart_doughnut() {

	if(typeof(Chart) === 'undefined') { return; }

	console.log('init_chart_doughnut');

	if($('.canvasDoughnut').length) {

		var chart_doughnut_settings = {
			type: 'doughnut',
			tooltipFillColor: "rgba(51, 51, 51, 0.55)",
			data: {
				labels: [
					"Symbian",
					"Blackberry",
					"Other",
					"Android",
					"IOS"
				],
				datasets: [{
					data: [15, 20, 30, 10, 30],
					backgroundColor: [
						"#BDC3C7",
						"#9B59B6",
						"#E74C3C",
						"#26B99A",
						"#3498DB"
					],
					hoverBackgroundColor: [
						"#CFD4D8",
						"#B370CF",
						"#E95E4F",
						"#36CAAB",
						"#49A9EA"
					]
				}]
			},
			options: {
				legend: false,
				responsive: false
			}
		}

		$('.canvasDoughnut').each(function() {

			var chart_element = $(this);
			var chart_doughnut = new Chart(chart_element, chart_doughnut_settings);

		});

	}

}

/*天气图标*/
function init_skycons() {

	if(typeof(Skycons) === 'undefined') { return; }
	console.log('init_skycons');

	var icons = new Skycons({
			"color": "#73879C"
		}),
		list = [
			"clear-day", "clear-night", "partly-cloudy-day",
			"partly-cloudy-night", "cloudy", "rain", "sleet", "snow", "wind",
			"fog"
		],
		i;

	for(i = list.length; i--;)
		icons.set(list[i], list[i]);

	icons.play();

}
/*首页计量器-（类汽车表盘）*/
function init_gauge() {

	if(typeof(Gauge) === 'undefined') { return; }

	console.log('init_gauge [' + $('.gauge-chart').length + ']');

	console.log('init_gauge');

	var chart_gauge_settings = {
		lines: 12,
		angle: 0,
		lineWidth: 0.4,
		pointer: {
			length: 0.75,
			strokeWidth: 0.042,
			color: '#1D212A'
		},
		limitMax: 'false',
		colorStart: '#1ABC9C',
		colorStop: '#1ABC9C',
		strokeColor: '#F0F3F3',
		generateGradient: true
	};

	if($('#chart_gauge_01').length) {

		var chart_gauge_01_elem = document.getElementById('chart_gauge_01');
		var chart_gauge_01 = new Gauge(chart_gauge_01_elem).setOptions(chart_gauge_settings);

	}

	if($('#gauge-text').length) {

		chart_gauge_01.maxValue = 6000;
		chart_gauge_01.animationSpeed = 32;
		chart_gauge_01.set(3200);
		chart_gauge_01.setTextField(document.getElementById("gauge-text"));

	}

	if($('#chart_gauge_02').length) {

		var chart_gauge_02_elem = document.getElementById('chart_gauge_02');
		var chart_gauge_02 = new Gauge(chart_gauge_02_elem).setOptions(chart_gauge_settings);

	}

	if($('#gauge-text2').length) {

		chart_gauge_02.maxValue = 9000;
		chart_gauge_02.animationSpeed = 32;
		chart_gauge_02.set(2400);
		chart_gauge_02.setTextField(document.getElementById("gauge-text2"));

	}

}

	/* SELECT2 */
	  
		function init_select2() {
			
			if( typeof ($.fn.select2) === 'undefined'){ return; }
			console.log('init_toolbox_select2');
//			  $.fn.select2.defaults.set("language","zh-CN");无效
			$(".select2_single").select2({
			  placeholder: "选择状态",
			  allowClear: true
			});
			$(".select2_group").select2({});
			$(".select2_multiple").select2({
			  maximumSelectionLength: 2,
			  placeholder: "最多选择2个",
			  allowClear: true,
			  language:"zh-CN"
			});
			
		};
		
		 /* AUTOSIZE jQuery Autosize是一个能够自动调整Textarea高度的jQuery插件。随着输入的字数越来越多，Textarea高度将自动变高。*/
			
		function init_autosize() {
			
			if(typeof autosize !== 'undefined'){
			 console.log('init_texarea_autosize');
			 autosize($('.resizable_textarea'));
			
			}
			
		};
		
			   /* AUTOCOMPLETE  输入框自动完成*/
			
		function init_autocomplete() {
			
			if(typeof $.fn.autocomplete === 'undefined'){ return; }
			console.log('init_autocomplete');
			
			var countries = { AD:"Andorra",A2:"Andorra Test",AE:"United Arab Emirates",AF:"Afghanistan",AG:"Antigua and Barbuda",AI:"Anguilla",AL:"Albania",AM:"Armenia",AN:"Netherlands Antilles",AO:"Angola",AQ:"Antarctica",AR:"Argentina",AS:"American Samoa",AT:"Austria",AU:"Australia",AW:"Aruba",AX:"Åland Islands",AZ:"Azerbaijan",BA:"Bosnia and Herzegovina",BB:"Barbados",BD:"Bangladesh",BE:"Belgium",BF:"Burkina Faso",BG:"Bulgaria",BH:"Bahrain",BI:"Burundi",BJ:"Benin",BL:"Saint Barthélemy",BM:"Bermuda",BN:"Brunei",BO:"Bolivia",BQ:"British Antarctic Territory",BR:"Brazil",BS:"Bahamas",BT:"Bhutan",BV:"Bouvet Island",BW:"Botswana",BY:"Belarus",BZ:"Belize",CA:"Canada",CC:"Cocos [Keeling] Islands",CD:"Congo - Kinshasa",CF:"Central African Republic",CG:"Congo - Brazzaville",CH:"Switzerland",CI:"Côte d’Ivoire",CK:"Cook Islands",CL:"Chile",CM:"Cameroon",CN:"China",CO:"Colombia",CR:"Costa Rica",CS:"Serbia and Montenegro",CT:"Canton and Enderbury Islands",CU:"Cuba",CV:"Cape Verde",CX:"Christmas Island",CY:"Cyprus",CZ:"Czech Republic",DD:"East Germany",DE:"Germany",DJ:"Djibouti",DK:"Denmark",DM:"Dominica",DO:"Dominican Republic",DZ:"Algeria",EC:"Ecuador",EE:"Estonia",EG:"Egypt",EH:"Western Sahara",ER:"Eritrea",ES:"Spain",ET:"Ethiopia",FI:"Finland",FJ:"Fiji",FK:"Falkland Islands",FM:"Micronesia",FO:"Faroe Islands",FQ:"French Southern and Antarctic Territories",FR:"France",FX:"Metropolitan France",GA:"Gabon",GB:"United Kingdom",GD:"Grenada",GE:"Georgia",GF:"French Guiana",GG:"Guernsey",GH:"Ghana",GI:"Gibraltar",GL:"Greenland",GM:"Gambia",GN:"Guinea",GP:"Guadeloupe",GQ:"Equatorial Guinea",GR:"Greece",GS:"South Georgia and the South Sandwich Islands",GT:"Guatemala",GU:"Guam",GW:"Guinea-Bissau",GY:"Guyana",HK:"Hong Kong SAR China",HM:"Heard Island and McDonald Islands",HN:"Honduras",HR:"Croatia",HT:"Haiti",HU:"Hungary",ID:"Indonesia",IE:"Ireland",IL:"Israel",IM:"Isle of Man",IN:"India",IO:"British Indian Ocean Territory",IQ:"Iraq",IR:"Iran",IS:"Iceland",IT:"Italy",JE:"Jersey",JM:"Jamaica",JO:"Jordan",JP:"Japan",JT:"Johnston Island",KE:"Kenya",KG:"Kyrgyzstan",KH:"Cambodia",KI:"Kiribati",KM:"Comoros",KN:"Saint Kitts and Nevis",KP:"North Korea",KR:"South Korea",KW:"Kuwait",KY:"Cayman Islands",KZ:"Kazakhstan",LA:"Laos",LB:"Lebanon",LC:"Saint Lucia",LI:"Liechtenstein",LK:"Sri Lanka",LR:"Liberia",LS:"Lesotho",LT:"Lithuania",LU:"Luxembourg",LV:"Latvia",LY:"Libya",MA:"Morocco",MC:"Monaco",MD:"Moldova",ME:"Montenegro",MF:"Saint Martin",MG:"Madagascar",MH:"Marshall Islands",MI:"Midway Islands",MK:"Macedonia",ML:"Mali",MM:"Myanmar [Burma]",MN:"Mongolia",MO:"Macau SAR China",MP:"Northern Mariana Islands",MQ:"Martinique",MR:"Mauritania",MS:"Montserrat",MT:"Malta",MU:"Mauritius",MV:"Maldives",MW:"Malawi",MX:"Mexico",MY:"Malaysia",MZ:"Mozambique",NA:"Namibia",NC:"New Caledonia",NE:"Niger",NF:"Norfolk Island",NG:"Nigeria",NI:"Nicaragua",NL:"Netherlands",NO:"Norway",NP:"Nepal",NQ:"Dronning Maud Land",NR:"Nauru",NT:"Neutral Zone",NU:"Niue",NZ:"New Zealand",OM:"Oman",PA:"Panama",PC:"Pacific Islands Trust Territory",PE:"Peru",PF:"French Polynesia",PG:"Papua New Guinea",PH:"Philippines",PK:"Pakistan",PL:"Poland",PM:"Saint Pierre and Miquelon",PN:"Pitcairn Islands",PR:"Puerto Rico",PS:"Palestinian Territories",PT:"Portugal",PU:"U.S. Miscellaneous Pacific Islands",PW:"Palau",PY:"Paraguay",PZ:"Panama Canal Zone",QA:"Qatar",RE:"Réunion",RO:"Romania",RS:"Serbia",RU:"Russia",RW:"Rwanda",SA:"Saudi Arabia",SB:"Solomon Islands",SC:"Seychelles",SD:"Sudan",SE:"Sweden",SG:"Singapore",SH:"Saint Helena",SI:"Slovenia",SJ:"Svalbard and Jan Mayen",SK:"Slovakia",SL:"Sierra Leone",SM:"San Marino",SN:"Senegal",SO:"Somalia",SR:"Suriname",ST:"São Tomé and Príncipe",SU:"Union of Soviet Socialist Republics",SV:"El Salvador",SY:"Syria",SZ:"Swaziland",TC:"Turks and Caicos Islands",TD:"Chad",TF:"French Southern Territories",TG:"Togo",TH:"Thailand",TJ:"Tajikistan",TK:"Tokelau",TL:"Timor-Leste",TM:"Turkmenistan",TN:"Tunisia",TO:"Tonga",TR:"Turkey",TT:"Trinidad and Tobago",TV:"Tuvalu",TW:"Taiwan",TZ:"Tanzania",UA:"Ukraine",UG:"Uganda",UM:"U.S. Minor Outlying Islands",US:"United States",UY:"Uruguay",UZ:"Uzbekistan",VA:"Vatican City",VC:"Saint Vincent and the Grenadines",VD:"North Vietnam",VE:"Venezuela",VG:"British Virgin Islands",VI:"U.S. Virgin Islands",VN:"Vietnam",VU:"Vanuatu",WF:"Wallis and Futuna",WK:"Wake Island",WS:"Samoa",YD:"People's Democratic Republic of Yemen",YE:"Yemen",YT:"Mayotte",ZA:"South Africa",ZM:"Zambia",ZW:"Zimbabwe",ZZ:"Unknown or Invalid Region" };

			var countriesArray = $.map(countries, function(value, key) {
			  return {
				value: value,
				data: key
			  };
			});

			// initialize autocomplete with custom appendTo
			$('#autocomplete-custom-append').autocomplete({
			  lookup: countriesArray
			});
			
		};

/* STARRR 星级评分*/
			
	function init_starrr() {
		
		if(typeof $.fn.starrr === 'undefined'){ return; }
		console.log('init_starrr');
		
		$(".stars").starrr();

		$('.stars-existing').starrr({
		  rating: 4
		});

		$('.stars').on('starrr:change', function (e, value) {
		  $('.stars-count').html(value);
		});

		$('.stars-existing').on('starrr:change', function (e, value) {
		  $('.stars-count-existing').html(value);
		});
		
	  };
	  
	  	   /* WYSIWYG EDITOR 富文本编辑器*/

		function init_wysiwyg() {
			
		if( typeof ($.fn.wysiwyg) === 'undefined'){ return; }
		console.log('init_wysiwyg');	
			
        function init_ToolbarBootstrapBindings() {
        	//设置字体
          var fonts = ['Serif', 'Sans', 'Arial', 'Arial Black', 'Courier',
              'Courier New', 'Comic Sans MS', 'Helvetica', 'Impact', 'Lucida Grande', 'Lucida Sans', 'Tahoma', 'Times',
              'Times New Roman', 'Verdana'
            ],
            fontTarget = $('[title=Font]').siblings('.dropdown-menu');
          $.each(fonts, function(idx, fontName) {
            fontTarget.append($('<li><a data-edit="fontName ' + fontName + '" style="font-family:\'' + fontName + '\'">' + fontName + '</a></li>'));
          });
          
          $('a[title]').tooltip({
            container: 'body'
          });
          $('.dropdown-menu input').click(function() {
              return false;
            })
            .change(function() {
              $(this).parent('.dropdown-menu').siblings('.dropdown-toggle').dropdown('toggle');
            })
            .keydown('esc', function() {
              this.value = '';
              $(this).change();
            });

          $('[data-role=magic-overlay]').each(function() {
            var overlay = $(this),
              target = $(overlay.data('target'));
            overlay.css('opacity', 0).css('position', 'absolute').offset(target.offset()).width(target.outerWidth()).height(target.outerHeight());
          });

          if ("onwebkitspeechchange" in document.createElement("input")) {
            var editorOffset = $('#editor').offset();

            $('.voiceBtn').css('position', 'absolute').offset({
              top: editorOffset.top,
              left: editorOffset.left + $('#editor').innerWidth() - 35
            });
          } else {
            $('.voiceBtn').hide();
          }
        }

        function showErrorAlert(reason, detail) {
          var msg = '';
          if (reason === 'unsupported-file-type') {
            msg = "Unsupported format " + detail;
          } else {
            console.log("error uploading file", reason, detail);
          }
          $('<div class="alert"> <button type="button" class="close" data-dismiss="alert">&times;</button>' +
            '<strong>File upload error</strong> ' + msg + ' </div>').prependTo('#alerts');
        }

       $('.editor-wrapper').each(function(){
			var id = $(this).attr('id');	//editor-one
			
			$(this).wysiwyg({
				toolbarSelector: '[data-target="#' + id + '"]',
				fileUploadError: showErrorAlert
			});	
		});
 		
 		$('#editor').wysiwyg();
		
        window.prettyPrint;
        prettyPrint();
    };
	 


$(document).ready(function() {
	init_flot_chart();//时间线，波形图
	init_sidebar();//侧栏
	init_InputMask();//格式化录入插件
	init_TagsInput();//标签插件
	init_parsley();//校验插件
	init_daterangepicker(); //日期范围选择初始化
	init_compose();//jquery 开关切换
	init_chart_doughnut();//圆环图
	init_skycons();//天气
	init_gauge();//汽车表盘
	init_select2();//下拉框插件
	init_autosize();//自动调整Textarea高度
	init_autocomplete();//输入框自动完成
	init_starrr();//星级评分
	init_wysiwyg();//富文本编辑器
});