var app = angular.module("app", []);

app.controller("serviceController", ['$scope', function ($scope) {
    $scope.regex = /[^\/\:\<\>\+\-\?\!\~\=\-\_\;]$/;
    $scope.regexPhone = /^(?:0|\(?\+84\)?\s?|[1-79]\s?)[1-79](?:[\.\-\s]?\d)\d{4,}$/;
    $scope.service = [
        {
            name: 'Ăn sáng',
            checked: false,
            readonly: false,
            id: 'option1'
        },
        {
            name: 'Trà sáng',
            checked: false,
            readonly: false,
            id: 'option2'
        },
        {
            name: 'Sọt củi',
            checked: false,
            readonly: false,
            id: 'option3'
        }
    ];

    $scope.setSelected = function (prop) {
        $scope.selectedprop = prop;
        if (prop.value == "BIỆT THỰ") {
            $scope.service = $scope.service.map(e => {
                return {
                    name: e.name,
                    checked: true,
                    readonly: false,
                    id: e.id
                }
            });
        } else if (prop.value == "BUNGALOW") {
            $scope.service = $scope.service.map(e => {
                if (e.name != 'Sọt củi') {
                    return {
                        name: e.name,
                        checked: true,
                        readonly: false,
                        id: e.id
                    }
                }
                return {
                    name: e.name,
                    checked: false,
                    readonly: true,
                    id: e.id
                }
                
            });
        } else {
            $scope.service = $scope.service.map(e => {
                return {
                    name: e.name,
                    checked: false,
                    readonly: false,
                    id: e.id
                }
            });
        }
};

$scope.props = [
  { label: "LỀU", value: "LỀU", per: false},
  { label: "KHÁCH SẠN", value: "KHÁCH SẠN", per: false},
  { label: "BUNGALOW", value: "BUNGALOW", per: false},
  { label: "BIỆT THỰ", value: "BIỆT THỰ", per: false},
  { label: "KHÁC", value: "", per: true }
];

$scope.setAdult = function (prop) {
    $scope.selectedAdult = prop;
    document.querySelector('input[name=adult]').value = prop.value;
};
$scope.Adult = [
  { label: "1 Người lớn", value: "1 Người lớn", per: false },
  { label: "2 Người lớn", value: "2 Người lớn", per: false },
  { label: "3 Người lớn", value: "3 Người lớn", per: false },
  { label: "Khác", value: "", per: true }
];

$scope.setChild = function (prop) {
    $scope.selectedChild = prop;
    document.querySelector('input[name=child]').value = prop.value;
};
$scope.Child = [
  { label: "1 Trẻ em", value: "1 Trẻ em", per: false },
  { label: "2 Trẻ em", value: "2 Trẻ em", per: false },
  { label: "3 Trẻ em", value: "3 Trẻ em", per: false },
  { label: "Khác", value: "", per: true }
];

$scope.activty = [
{
    name: 'Yoga',
    checked: false,
    id: 'act1'
},
{
    name: 'Spa',
    checked: false,
    id: 'act2'
},
{
    name: 'Bicycle Riding',
    checked: false,
    id: 'act3'
},
{
    name: 'Karaoke',
    checked: false,
    id: 'act4'
},
{
    name: 'Dancing',
    checked: false,
    id: 'act5'
}
];
},
]);
// room page
app.controller("utilities", ['$scope',
  function ($scope) {
      $scope.icons = [
        { icon: "bath" },
        { icon: "bed" },
        { icon: "book" },
        { icon: "laptop" },
        { icon: "cutlery" },
        { icon: "feed" },
        { icon: "gamepad" },
        { icon: "television" },
      ];
  },
]);

app.controller("startdate", ['$scope',
  function ($scope) {
      $scope.regex = /^(?:19|20)[0-9]{2}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-9])|(?:(?!02)(?:0[1-9]|1[0-2])-(?:30))|(?:(?:0[13578]|1[02])-31))$/;
      $scope.regexDay = /[^\/\:\<\>\+\-\?\!\~\=\-\_\;]$/;
      $scope.startdate = new Date(Date.now());
      $scope.today = new Date(Date.now());
      $scope.timeMain;
      $scope.changeStart = function () {
          $scope.timeMain = $scope.compare();
      };
      $scope.changeEnd = function () {
          $scope.timeMain = $scope.compare();
      };
      $scope.compare = function () {
          let result,
              a = document.querySelector('input[name="startdate"]').value,
              b = document.querySelector('input[name="enddate"]').value;
          if ((a && b) != "") {
              let c = new Date(a),
                  d = new Date(b),
                  oneDay = 24 * 60 * 60 * 1000,
                  check = ((d.getTime() - c.getTime()) / (oneDay)),
                  diffDays = (check >= 0) ? Math.ceil(check) : -1;
              if (diffDays == 0) {
                  return "Đi/về trong ngày";
              } else if (diffDays > 0) {
                  return `${diffDays} Ngày`;
              } else {
                  return `Ngày không hợp lệ vui lòng chọn lại ngày đi và về`;
              }
          }
      }
  },
]);

app.controller("listRoomChk", ['$scope',
  function ($scope) {
      $scope.items = [
        'VILLA',
        'BUNGALOW',
        'CAMPING',
        'ROOM'
      ];
  },
]);

app.controller("listRoom", ['$scope',
  function ($scope) {
      $scope.items = [];
      for (var i = 0; i < 5; i++) {
          $scope.items.push(i);
      }
  },
]);

app.controller("listServiceChk", ['$scope',
  function ($scope) {
      $scope.items = [
        'Dịch vụ ăn, tổ chức tiệc',
        'Dịch vụ mua sắm',
        'Các dich vụ phổ thông khác',
        'Các tour chuyên nghiệp'
      ];
      $scope.checkboxModel = [
        {
            value: true
        },
        {
            value: false
        },
        {
            value: false
        },
        {
            value: false
        },
      ];
  },
]);

app.controller("listService", ['$scope',
  function ($scope) {
      $scope.items = [
        {
            label: "Dịch vụ ăn, tổ chức tiệc", subService: [
            {
                name: "Ăn sáng",
                description: "Trong nhịp sống hiện đại bận rộn, con người thường có xu hướng tìm về thiên nhiên để thanh lọc cơ thể và tìm lại sự bình yên trong tâm hồn."
            },
            {
                name: "Thực dưỡng",
                description: "Trong nhịp sống hiện đại bận rộn, con người thường có xu hướng tìm về thiên nhiên để thanh lọc cơ thể và tìm lại sự bình yên trong tâm hồn."
            },
            {
                name: "Tiệc nướng",
                description: "Xây dựng các giếng nhỏ để làm tiệc nướng theo kiểu Tây Tạng"
            },
            {
                name: "Buffet rau, buffet ngọt",
                description: "Hoạt động làng nghề để làm đặc sản Đà Lạt làm buffet ngọt, sử dụng sản phẩm trồng trong vườn"
            }
            ]
        },
        {
            label: "Dịch vụ mua sắm", subService: [
            {
                name: "Mua hàng có sẵn tại khu nghỉ dưỡng",
                description: "Tổ chức làng nghề đặc sản Đà Lạt như mứt, nước ép,  các sản phẩm trồng tại khu. Khu Farmshop tại Đà Lạt"
            },
            {
                name: "Đưa đi mua sắm, mua ngoài",
                description: "Kết hợp cùng với hướng dẫn viên đưa khách hàng đi tham quan bên ngoài bao gồm đưa đón cho thuê xe và hướng dẫn nếu cần thiết"
            }
            ]
        },
        {
            label: "Các dich vụ phổ thông khác", subService: [
            {
                name: "Dịch vụ giặt, không có dịch vụ ủi",
                description: "Cung cấp bàn ủi cho khách nếu có nhu cầu,bàn để ủi mỗi phòng"
            },
            {
                name: "Dịch vụ thuê xe máy, oto tự lái, oto có người lái",
                description: "Kết hợp cùng với hướng dẫn viên đưa khách hàng đi tham quan bên ngoài bao gồm đưa đón cho thuê xe và hướng dẫn nếu cần thiết"
            },
            {
                name: "Dịch vụ đưa đón khách",
                description: "Kết hợp cùng với hướng dẫn viên đưa khách hàng đi tham quan bên ngoài bao gồm đưa đón cho thuê xe và hướng dẫn nếu cần thiết"
            },
            {
                name: "Dịch vụ giữ trẻ",
                description: "Bao gồm trong dịch vụ dưỡng tâm; và tách rời với các dịch vụ khác"
            }
            ]
        },
        {
            label: "Các tour chuyên nghiệp", subService: [
            {
                name: "Dưỡng tâm tour",
                description: "Kết hợp cùng với hướng dẫn viên đưa khách hàng đi tham quan bên ngoài bao gồm đưa đón cho thuê xe và hướng dẫn nếu cần thiết. Ăn: thực phẩm dưỡng sinh; có bác sĩ chỉ định; chế độ đặc biệt được thiết lập sẵn. Dịch vụ: yoga( hằng ngày); đọc sách; chơi cờ; Giao lưu với chuyên gia, luật sư, bất động sản, kiến trúc sư."
            },
            {
                name: "Tour nghỉ dưỡng, Family Tour",
                description: `Càng rời xa thiên nhiên tâm tánh con người ngày càng trở nên hỗn loạn và bạo động. Sự hài
                        hòa, thanh thoát của thiên nhiên sẽ giúp chúng ta xua tan những phiền não: xôn xao và lo âu
                        làm cho tâm hồn yên lắng trở lại. Nếu được một lần đứng trước thiên nhiên hùng vĩ, ta mới
                        thấy cái bản ngã của con người mới nhỏ bé làm sao!. Những tính toán hơn thua, giận hờn
                        hôm qua hay ban nãy đây thật vô lý. Thiên nhiên khoáng đạt thế, đời người được bao lâu.
                        Nếu tưởng tượng một mình lạc lõng giữa núi rừng hoang sơ không một bóng người, ta sẽ
                        cảm giác như thế nào nhỉ?`
            },
            {
                name: "Camping tour",
                description: "Kết hợp cùng với hướng dẫn viên đưa khách hàng đi tham quan bên ngoài bao gồm đưa đón cho thuê xe và hướng dẫn nếu cần thiết. Ăn: thực phẩm dưỡng sinh; có bác sĩ chỉ định; chế độ đặc biệt được thiết lập sẵn. Dịch vụ: yoga( hằng ngày); đọc sách; chơi cờ; Giao lưu với chuyên gia, luật sư, bất động sản, kiến trúc sư."
            },
            {
                name: "Children tour/ student tour",
                description: "Kết hợp cùng với hướng dẫn viên đưa khách hàng đi tham quan bên ngoài bao gồm đưa đón cho thuê xe và hướng dẫn nếu cần thiết. Ăn: thực phẩm dưỡng sinh; có bác sĩ chỉ định; chế độ đặc biệt được thiết lập sẵn. Dịch vụ: yoga( hằng ngày); đọc sách; chơi cờ; Giao lưu với chuyên gia, luật sư, bất động sản, kiến trúc sư."
            },
            {
                name: "Dưỡng tâm tour",
                description: "Tour dành cho sinh viên thực tập, ra trường thích khám phá, trẻ em của khách hàng tham gia tour dưỡng tâm. Có người hướng dẫn tham gia các hoạt động."
            },
            {
                name: "Working tour",
                description: "Làm việc; nghỉ dưỡng; hội nghị; ăn uống. Khởi nguồn sáng tạo cho doanh nhân, nhạc sĩ, …"
            },
            ]
        },
      ];
  },
]);

app.filter('myFormat', function () {
    return function (x) {
        var txt;
        if ((x != null) || (x != undefined)) {
            return txt = x.toString();
        }
    };
});

app.controller("checkModel", ['$scope',
  function ($scope) {
      // $scope.check = function(data)
      // {
      //   var flag = false;
      //   if(typeof data != "undefined") {
      //     if(data.indexOf("#0") != -1)
      //     {
      //       let $action = $("#activity");
      //       let $length = $action.children().length;
      //       let $action_span = $(".jcf-list-content ul");
      //       $action.append(`<option value="Hoạt động ${ $length + 1 }">Hoạt động ${ $length + 1 }</option>`);
      //       $action_span.append(`<li><span class="jcf-option" data-index="${ $length + 1 }">Hoạt động ${ $length + 1 }</span></li>`);
      //       return false;
      //     }
      //   }
      //   return false;
      // };
  }
]);
