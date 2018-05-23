
var app = angular.module('adsApp', []);
app.controller('adsCtrl', function ($scope, $http) {
    $scope.currentAdvert = {};
    $scope.products = [];
    $scope.slider = null;

    $scope.getAdvert = function (advertId) {
        $http({
            url: "../advert/getadvert",
            method: 'GET',
            params: { advertId: advertId }
        }).then(function (response) {
            $scope.currentAdvert = response.data;
            setTimeout(function () {
                if ($scope.slider) {
                    $scope.slider.destroySlider();
                }
                $scope.slider = $('.slider').bxSlider({
                    pager: false,
                    auto: true,
                    maxSlides: 1,
                    moveSlides: 1,
                    adaptiveHeight: false,
                });
            }, 100)
        }, function (response) {
            console.log(response);
        });
    };


    $scope.saveAdvert = function () {
        $http({
            url: "../advert/saveadvert",
            method: 'POST',
            params: $scope.currentAdvert
        }).then(function (response) {
            $scope.currentAdvert = response.data;
        }, function (response) {
            console.log(response);
        });
    };

    $scope.getProducts = function () {
        $http({
            url: "../products/getall",
            method: 'GET',
        }).then(function (response) {
            $scope.products = response.data;
        }, function (response) {
            console.log(response);
        });
    };

    $scope.upload = function (element) {
        var formData = new FormData();
        var file = document.getElementById("Logo").files[0];
        formData.append("UploadedImage", file);

        var ajaxRequest = $.ajax({
            type: "POST",
            url: '../advert/upload',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false
        });

        ajaxRequest.done(function (xhr, textStatus) {
            console.log(textStatus);
        });

    }

    $scope.getProducts();
    $scope.getAdvert(1);

    $scope.$watch('currentAdvert.PriceFormat', function (newVal, oldVal) {
        angular.forEach($scope.products, function (product, key) {
            if ($scope.currentAdvert && $scope.currentAdvert.PriceFormat) {
                product.PriceCurrency = $scope.currentAdvert.PriceFormat.replace("{currency}", product.Currency).replace("{price}", product.Price);
            }
        });
    });
});
