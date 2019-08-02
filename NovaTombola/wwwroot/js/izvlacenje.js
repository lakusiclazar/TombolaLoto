
function startLotto() {
    while (lotteryNumbers.length < 7) {
        const newBr = chooseNumber();
        if (!lotteryNumbers.includes(newBr)) {
            lotteryNumbers.push(newBr);
        }
    }

    var winnersList = usersArray;
    for (let i = 0; i < lotteryNumbers.length; i++) {
        if (!winnersList.length) {
            break;
        }
        winnersList = winnersList.filter(pl => pl.combination.includes(lotteryNumbers[i]));
    }
    if (!winnersList.length) {
        document.getElementById('lotto-result').textContent = lotteryNumbers.toString();
        lotteryNumbers = [];
        setTimeout(() => {
            startLotto();
        }, 0);
    } else {
        document.getElementById('winnerName').textContent = winnersList.map(item => item.name).toString();
        document.getElementById('winnerNumbers').textContent = lotteryNumbers.toString();
        document.getElementById('lotto-game').classList.add('invisible');
        document.getElementById('lotto-finish').classList.add('visible');

        localStorage.setItem('winners', JSON.stringify(winnersList));
        localStorage.removeItem('combinationList');
        winnersList = [];
        usersArray = [];
    }
}

function chooseNumber() {
    return Math.floor(Math.random() * 36) + 1;
}



//function Izvlacenje(size, lowest, highest) {
//    var numbers = [];
//    for (var i = 0; i < size; i++) {
//        var add = true;
//        var randomNumber = Math.floor(Math.random() * highest) + 1;
//        for (var y = 0; y < highest; y++) {
//            if (numbers[y] == randomNumber) {
//                add = false;
//            }
//        }

//        if (add) {
//            numbers.push(randomNumber);
//        } else {
//            i--;
//        }
//    }

//    var highestNumber = 0;
//    for (var m = 0; m < numbers.length; m++) {
//        for (var n = m + 1; n < numbers.length; n++) {
//            if (numbers[n] < numbers[m]) {
//                highestNumber = numbers[m];
//                numbers[m] = numbers[n];
//                numbers[n] = highestNumber;
//            }
//        }
//    }
//    document.getElementById("numbers").innerHTML = numbers.join(" ");

//    var listaSvihKombinacija = $("#kombinacije").val()

//    var listaKombinacija = listaSvihKombinacija.split("|");
//    var duzinaKombinacija = Math.ceil(listaKombinacija.length);


//    var numbers1 = numbers.toString();
//    var a = 6;

//    do {
//        var kombinacija = listaKombinacija.splice(i);

//        a = + 6;
//        i++;
//    } while (i < duzinaKombinacija);


//    var i, j, temparray; chunk = 10;
//    for (i = 0, j = array.length; i < j; i += chunk) {
//        temparray = array.slice(i, i + chunk);
//         do whatever
//    }

//}