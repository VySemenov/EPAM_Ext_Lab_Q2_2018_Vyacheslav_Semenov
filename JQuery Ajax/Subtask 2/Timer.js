var timeout;
var interval;
var startIndex = 0;
var startInterval = 5000;
var timerStep = 100;
var timeRest = 0;
var paused = false;

start();

function start() {
    timeout = setTimeout(move, startInterval);
    interval = setInterval(changeTimer, timerStep);
}

function move() {
    var index = parseInt(document.getElementById("pageId").innerText);
    var pages = ["Index.html", "Page1.html", "Page2.html", "Page3.html"];
    
    if (index != pages.length - 1)
        window.location = pages[index + 1];
    else {
        if (confirm("Repeat?"))
            window.location = pages[startIndex];
        else{
            StartPauseTimer();
            window.close();
        }
    }
}
function changeTimer() {
    if (timeRest <= 0)
        timeRest = startInterval;
    timeRest = timeRest - timerStep;
    document.getElementById("timer").innerHTML = "Timer: " + (timeRest / 1000) + " sec";
}
function StartPauseTimer() {
    if (paused) {
        start();
        timeRest = 0;
        paused = false;
    }
    else {
        clearInterval(interval);
        clearTimeout(timeout);
        paused = true;
    }
}

function previous() {
    history.back(); 
}
function next() {
    move();
}