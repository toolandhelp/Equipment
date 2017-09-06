(function () {

//    菜单hover效果
  var menus = $('.menu-link');
  menus.on('mouseover', function () {
    menus.removeClass('active');
    $(this).addClass('active');
  });


//    地图轮播


  var mapSwiper = new Swiper('.swiper-container', {
    pagination: '.pagination',
    loop: true,
    grabCursor: true,
    paginationClickable: true,
  });


//    切换图表表格

  $('.change-table').on('click', function () {
    $(this).parent().children('.chart-table').toggleClass('show');
  })
//    折线图

  var projectLineChart = echarts.init(document.getElementById('line-box'), {
    renderer: 'canvas',
    width: 'auto',
    height: 'auto'
  });
  var projecOption = {

    tooltip: {
      trigger: 'axis'
    },
    legend: {
      data: ['新建项目', '运维项目']
    },
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      containLabel: true
    },
    xAxis: {
      type: 'category',
      boundaryGap: false,
      data: ['2011年', '2012年', '2013年', '2014年', '2015年', '2016年', '2017年']
    },
    yAxis: {
      type: 'value'
    },
    series: [
      {
        name: '新建项目',
        type: 'line',
        stack: '总量',
        data: [120, 132, 101, 134, 90, 230, 210]
      },
      {
        name: '运维项目',
        type: 'line',
        stack: '总量',
        data: [220, 182, 191, 234, 290, 330, 310]
      }

    ]
  };
  projectLineChart.setOption(projecOption);

  //资源编目 柱状图

  var zybmhAxisData = [], zybmseriesData = [];
 // var tabhtml = "<div class='chart-table'><table><tr><th>单位</th></tr><tr><td></td></tr>";
  var trhtml = "<th>单位</th>";
  var tdhtml = "<td>编目数</td>";

  $.ajax({
      url: 'ZYBMApi',
      type: 'POST',
      dataType: "json",
      async: false,
      beforeSend: function () { $("#bar-box").html("<div style='width:120px;margin: 0 auto;'><img src='/Images/loading.gif' width='32px' height='32px' align='absmiddle' /> 正在加载．．．</div>"); },
      ContentType: "application/json;charset=utf-8",
      success: function (responseList) {
          if (responseList.ErrorType == "1") {
              for (var i = 0; i < responseList.Entity.length; i++) {
                  zybmhAxisData.push(responseList.Entity[i].name);
                  zybmseriesData.push(responseList.Entity[i].value);

                  trhtml += "<th>" + responseList.Entity[i].name+"</th>";
                  tdhtml += "<td>" + responseList.Entity[i].value+"个</td>";
              }

              $("#bar-box-th").empty().append(trhtml);
              $("#bar-box-td").empty().append(tdhtml);//.append(tdhtml);
          }
      },
      error: function (xmlHttpRequest, textStatus, errorThrown) {
          MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
          $("#bar-box").html("数据加载失败,请稍后再试！");
      }
  });





  var barChart = echarts.init(document.getElementById('bar-box'), {
    renderer: 'canvas',
    width: 'auto',
    height: 'auto'
  });
  var barOption = {

    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    // formatter:'{c}%',
//      legend: {
//        data: ['2017年']
//      },
    grid: {
      top: '10%',
      left: '3%',
      right: '4%',
      bottom: '3%',
      containLabel: true
    },
    xAxis: {
      type: 'category',
      axisLabel: {'interval': 0},
      data: zybmhAxisData
    },
    yAxis: {

      type: 'value',
      boundaryGap: [0, 0.01]
    },
    series: [
      {
        name: '资源编目数量',
        type: 'bar',
        barWidth: '20px',
        data: zybmseriesData
      }
    ]
  };

  barChart.setOption(barOption);

  //硬件数量 柱状图





})()
