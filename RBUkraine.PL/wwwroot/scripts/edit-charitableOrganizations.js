//async function form_submit(action) {
////    $("#add_animal_form").append(`<input type="hidden" name="action" value="${action}" />`);
////    $("#add_animal_form").submit();
//    const species = document.querySelector("#species");
//    const latinSpecies = document.querySelector("#latinSpecies");
//    const conservationStatus = document.querySelector("#conservationStatus");
//
//    await fetch("https://localhost:5001/animals/create",
//        {
//            method: "post",
//            body: JSON.stringify({
//                species: species.value,
//                latinSpecies: latinSpecies.value
//            }),
//            headers: new Headers({ 'content-type': "application/json" }),
//            mode: "no-cors"
//        });
//}