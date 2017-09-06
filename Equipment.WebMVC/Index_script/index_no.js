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
    $(this).toggleClass('close');
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
      data: ['区科委', '区财政局', '区民政局', '区卫计局', '区公安局', '区教育局', '区检察院', '区法院', '区建交委', '区残联']
    },
    yAxis: {

      type: 'value',
      boundaryGap: [0, 0.01]
    },
    series: [
      {
        name: '2017年',
        type: 'bar',
        barWidth: '20px',
        data: [10, 9, 8, 7, 6, 5, 4, 3, 2, 1]
      }
    ]
  };

  barChart.setOption(barOption);

  //硬件数量 柱状图


  var hardWareOption = {
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    formatter: '{c}%',

    // legend: {
    //   data: ['2017年']
    // },
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      top: '4%',
      containLabel: true
    },
    xAxis: {
      type: 'value',
      boundaryGap: [0, 0.01]

    },
    yAxis: {

      type: 'category',
      data: ['区教育局', '区卫计委', '区财政局', '区民政局', '区科委']
    },
    series: [
      {
        name: '2017年',
        type: 'bar',
        barWidth: '12px',
        data: ['6', '7', '8', '9', '10']
      }
    ]
  };
  var hardWareChart = echarts.init(document.getElementById('hardware-count'), {
    renderer: 'canvas',
    width: 'auto',
    height: 'auto'
  });
  hardWareChart.setOption(hardWareOption);

  var connectOption = {
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    formatter: '{c}%',

    // legend: {
    //   data: ['2017年']
    // },
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      top: '4%',
      containLabel: true
    },
    xAxis: {
      type: 'value',
      boundaryGap: [0, 0.01]

    },
    yAxis: {

      type: 'category',
      data: ['区教育局', '区卫计委', '区财政局', '区民政局', '区科委']
    },
    series: [
      {
        name: '2017年',
        type: 'bar',
        barWidth: '12px',
        data: ['6', '7', '8', '9', '10']
      }
    ]
  };
  var connectChart = echarts.init(document.getElementById('connect-count'), {
    renderer: 'canvas',
    width: 'auto',
    height: 'auto'
  });
  connectChart.setOption(connectOption);


  //软件统计 饼图
  var pieOption = {
    title: {
      text: '安装360数量',
      x: 'center'
    },
    tooltip: {
      trigger: 'item',
      formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    legend: {
      orient: 'vertical',
      left: 'left',
      data: ['已安装', '未安装']
    },
    series: [
      {
        name: '访问来源',
        type: 'pie',
        radius: '65%',
        center: ['50%', '60%'],
        data: [
          {value: 335, name: '已安装'},
          {value: 310, name: '未安装'}
        ],
        itemStyle: {
          emphasis: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  };

  var pieChart = echarts.init(document.getElementById('360-count'), {
    renderer: 'canvas',
    width: 'auto',
    height: 'auto'
  });
  pieChart.setOption(pieOption);


})()
