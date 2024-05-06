function dateToTimeString(ts) {
    var dt = new Date(ts);

    return "<span>" + dt.getHours() + ":" + zeroFormat(dt.getMinutes()) + ":" + zeroFormat(dt.getSeconds()) + "</span><strong>" + dt.getMilliseconds() + "</strong>";
}

function zeroFormat(n) {
    return n <= 9 ? '0' + n : n;
}

function runTimer(remoteTime, localeTime) {

    reloadDiff(remoteTime, localeTime);

    var counter = 0;
    var limitUpdating = 10 * 1000 / 50;

    setInterval(function () {

        counter += 1;

        document.getElementById("remoteFormat").innerHTML = dateToTimeString(remoteTime);
        remoteTime += 50;

        document.getElementById("localeFormat").innerHTML = dateToTimeString(localeTime);
        localeTime += 50;

        if (counter % limitUpdating == 0) {
            reloadDiff(remoteTime, localeTime);
            counter = 0;
        }

    }, 50);
}

function reloadDiff(remoteTime, localeTime) {

    var diffTime = "";

    if (localeTime > remoteTime)
        diffTime = "+" + (localeTime - remoteTime) / 1000;
    else if (localeTime < remoteTime)
        diffTime = "-" + (remoteTime - localeTime) / 1000;
    else
        diffTime = "0";

    document.getElementById("diffTime").innerHTML = diffTime;
}

function loadTime() {
    var start = Date.now();
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var end = Date.now();
            document.getElementById("remoteFormat").innerHTML = dateToTimeString(start);
            document.getElementById("localeFormat").innerHTML = dateToTimeString(end);

            var diff = (end - start) / 1000;
            document.getElementById("diffTime").innerHTML = diff;

            var remoteTime = parseInt(this.responseText);
            var localeTime = Date.now();
            runTimer(remoteTime, localeTime);
        }
    };
    xmlhttp.open("GET", "/time/now", true);
    xmlhttp.send();
}

window.onload = loadTime;