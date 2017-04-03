myapp.service('workservice', function ($http) {

    this.getallWork = function () {
        return $http.get("/api/WorkAPI/");
    }

    //saving work
    this.save = function (Work) {
        var request = $http({
            method: 'post',
            url: '/api/WorkAPI/',
            date: Work
        });
        return request;
    }

    //get work by ID
    this.get = function (WorkID) {
        //debugger;
        return $http.get("/api/WorkAPI/" + WorkID)
    }

    //update work records
    this.update = function (UpdateWorkID, Work) {
        //debugger;
        var updatereq = $http({
            method: 'put',
            url: "/api/WorkAPI/" + UpdateWorkID,
            data: Work
        });
        return updatereq;
    }

    //delete work
    this.delete = function (WorkID) {
        //debugger;
        var deletereq = $http({
            method: 'delete',
            url: "/api/WorkAPI/" + WorkID
        });
        return deletereq;
    }

});