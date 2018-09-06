function Move(where, leftid, rightid) {
    var rightList = document.getElementById(rightid);
    var leftList = document.getElementById(leftid);
    var changeList = [];

    if (where == "toLeft") {
        for (var i = 0; i < rightList.length; i++) {
            if (rightList.options[i].selected)
                changeList.push(rightList.options[i].value);
        }
    }
    else {
        for (var i = 0; i < leftList.length; i++) {
            if (leftList.options[i].selected)
                changeList.push(leftList.options[i].value);
        }
    }

    if (changeList.length == 0) {
        alert('Not selected');
    }
    else {
        var left = [];
        for (var i = 0; i < leftList.options.length; i++)
            left.push(leftList.options[i].value);
        var right = [];
        for (var i = 0; i < rightList.options.length; i++)
            right.push(rightList.options[i].value);

        console.log({ 'left': left, 'right': right, 'change': changeList });

        $.ajax({
            url: "Home/Move",
            type: "post",
            data: { 'left': left, 'right': right, 'change': changeList },
            success: (function (json) {
                console.log(json.leftList);
                while (leftList.length != 0)
                    leftList.removeChild(leftList.options[0]);
                for (var i = 0; i < json.leftList.length; i++) {
                    var option = document.createElement("option");
                    option.value = json.leftList[i];
                    option.text = json.leftList[i];
                    leftList.appendChild(option);
                }

                console.log(json.rightList);
                while (rightList.length != 0)
                    rightList.removeChild(rightList.options[0]);
                for (var i = 0; i < json.rightList.length; i++) {
                    var option = document.createElement("option");
                    option.value = json.rightList[i];
                    option.text = json.rightList[i];
                    rightList.appendChild(option);
                }
            }),
            dataType: "json"
        });
    }
}

function MoveAll(where, leftid, rightid) {
    var leftList = document.getElementById(leftid);
    var rightList = document.getElementById(rightid);

    for (var i = 0; i < leftList.options.length; i++) {
        leftList.options[i].selected = "selected";
    }
    for (var i = 0; i < rightList.options.length; i++) {
        rightList.options[i].selected = "selected";
    }

    Move(where, leftid, rightid);
}