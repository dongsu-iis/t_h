/* 
JavaScript
    引数(start)から引数(end)まで順番に表示する。ただし、以下の規則に従う。
    ・3で割り切れるなら"Fizz“
    ・5で割り切れるなら"Buzz“
    ・両方で割り切れるなら"FizzBuzz"
*/
function printFizzBuzz(start = 1, end = 100) {
    if (start <= end) {
        // 効率を考え、剰余の計算回数をなるべく減らす
        // 0が判定がFizzBuzzにならないように、条件式に入れる
        if (start !== 0 && start % 3 === 0) {

            if (start % 5 === 0) {
                console.log("FizzBuzz");
            } else {
                console.log("Fizz");
            }

        } else if (start !== 0 && start % 5 === 0) {
            console.log("Buzz");
        } else {
            console.log(start);
        }
        printFizzBuzz(start + 1, end);
    }
}

// 実行
printFizzBuzz(1, 100);