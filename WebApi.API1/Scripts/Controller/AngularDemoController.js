// Defining angularjs module
var app = angular.module('studentInfoModule', []);

// Defining angularjs Controller and injecting ProductsService
app.controller('demoCtrl', function ($scope, $http, ProductsService) {

    $scope.productsData = null;
    // Fetching records from the factory created at the bottom of the script file
    ProductsService.GetAllRecords().then(function (d) {
        $scope.productsData = d.data; // Success
    }, function () {
        alert('Error Occured !!!'); // Failed
    });

    // Calculate Total of Price After Initialization
    //$scope.total = function () {
    //    var total = 0;
    //    angular.forEach($scope.productsData, function (item) {
    //        total += item.Price;
    //    })
    //    return total;
    //}

    $scope.Product = {
        id: '',
        firstName: '',
        lastName: '',
        address: '',
        dob: ''
    };

    // Reset product details
    $scope.clear = function () {
        $scope.Product.id = '';
        $scope.Product.firstName = '';
        $scope.Product.lastName = '';
        $scope.Product.address = '';
        $scope.Product.dob = '';
    }

    //Add New Item
    $scope.save = function () {
        if ($scope.Product.firstName != "" &&
       $scope.Product.lastName != "" && $scope.Product.address != "" && $scope.Product.dob != "") {
            // Call Http request using $.ajax

            //$.ajax({
            //    type: 'POST',
            //    contentType: 'application/json; charset=utf-8',
            //    data: JSON.stringify($scope.Product),
            //    url: 'api/Product/PostProduct',
            //    success: function (data, status) {
            //        $scope.$apply(function () {
            //            $scope.productsData.push(data);
            //            alert("Product Added Successfully !!!");
            //            $scope.clear();
            //        });
            //    },
            //    error: function (status) { }
            //});

            // or you can call Http request using $http
            $http({
                method: 'POST',
                url: 'api/studentInfo/',
                data: $scope.Product
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                //$scope.productsData.push(response.data);
                if (response.data != "Error") {
                    $scope.clear();
                    ProductsService.GetAllRecords().then(function (d) {
                        $scope.productsData = d.data; // Success
                    }, function () {
                        alert('Error Occured !!!'); // Failed
                    });
                    alert("Product Added Successfully !!!");
                } else {
                    alert("Error");
                }
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Edit product details
    $scope.edit = function (data) {
        $scope.Product = { id: data.id, firstName: data.firstName, lastName: data.lastName, address: data.address, dob: data.dob };
    }

    // Cancel product details
    $scope.cancel = function () {
        $scope.clear();
    }

    // Update product details
    $scope.update = function () {
        if ($scope.Product.firstName != "" &&
       $scope.Product.lastName != "" && $scope.Product.address != "" && $scope.Product.dob != "") {
            $http({
                method: 'PUT',
                url: 'api/studentInfo/',
                data: $scope.Product
            }).then(function successCallback(response) {
                if (response.data != "Error") {
                    $scope.clear();
                    ProductsService.GetAllRecords().then(function (d) {
                        $scope.productsData = d.data; // Success
                    }, function () {
                        alert('Error Occured !!!'); // Failed
                    });
                    alert("Product Updated Successfully !!!");
                } else {
                    alert("Error");
                }
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete product details
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: 'api/studentInfo/Delete/' + $scope.productsData[index].id,
        }).then(function successCallback(response) {
            if (response.data != "Error") {
                $scope.productsData.splice(index, 1);
                alert("Product Deleted Successfully !!!");
                $scope.clear();
            } else {
                alert("Error");
            }
        }, function errorCallback(response) {
            alert("Error : " + response.data.ExceptionMessage);
        });
    };

});

// Here I have created a factory which is a popular way to create and configure services.
// You may also create the factories in another script file which is best practice.

app.factory('ProductsService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('api/studentInfo/');
    }
    return fac;
});