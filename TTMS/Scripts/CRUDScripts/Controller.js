myapp.controller('workcontroller', function ($scope, workservice) {

    //For fetching data
    loadworks();
    function loadworks() {
        var works = workservice.getallWork();
        works.then(function (d) { //success
            $scope.Works = d.data;
        },
        function () { // fail
            swal("Oops..", "some problem in gethering data from workcontroller or jscontroller service", 'ERROR');
        })
    }

    //For saving data
    $scope.save = function () {
        //$scope.date = new date();
        var Work = {
            PK_Work: $scope.PK_Work,
            WorkType: $scope.WorkType,
            WorkPriority: $scope.WorkPriority,
            WorkTitle: $scope.WorkTitle,
            WorkDescr: $scope.WorkDescr,
            CreationDate: $scope.CreationDate,
            Deadline: $scope.Deadline
        };

        var savework = workservice.save(Work);
        savework.then(function (d) {
            loadworks();
            swal("Work inserted successfully");
        },
        function () {
            swal("Oops..", "some problem in saving work recoreds", 'ERROR');
        });
    }

    //get work by id
    $scope.get = function (Work) {
        var singlerecord = workservice.get(Work.WorkID);
        singlerecord.then(function (d) {

            var record = d.data;
            $scope.UpdatePK_Work = record.PK_Work,
            $scope.UpdateWorkType = recor.WorkType,
            $scope.UpdateWorkPriority = record.WorkPriority,
            $scope.UpdateWorkTitle = reco.WorkTitle,
            $scope.UpdateWorkDescr = record.WorkDescr,
            $scope.UpdateCreationDate = record.CreationDate,
            $scope.UpdateDeadline = record.Deadline
        },
        function () {
            swal("Oops..", "some problem in getting recored by ID", 'ERROR');
        }
        );
    }

    //update work data
    $scope.update = function () {
        var Work = {
            PK_Work: $scope.UpdatePK_Work,
            WorkType: $scope.UpdateWorkType,
            WorkPriority: $scope.UpdateWorkPriority,
            WorkTitle: $scope.UpdateWorkTitle,
            WorkDescr: $scope.UpdateWorkDescr,
            CreationDate: $scope.UpdateCreationDate,
            Deadline: $scope.UpdateDeadline
        };
        debugger;
        var updaterecords = workservice.update($scope.UpdatePK_Work, Work);
        updaterecords.then(function (d) {
            loadworks();
            swal("Records updated!!");
        },
        function () {
            swal("Oops..", "some problem in updating records", 'ERROR');
        });
    }

    //delete work data
    $scope.delete = function (UpdatePK_work) {
        debugger;
        var deleterecord = workservice.delete($scope.UpdatePK_work);
        deleterecord.then(function (d) {
            var Work = {
                PK_Work: '',
                WorkType: '',
                WorkPriority: '',
                WorkTitle: '',
                WorkDescr: '',
                CreationDate: '',
                Deadline: ''
            };
            loadworks();
            swal("records deleted succesfully!");
        });
    }

});