export function calculateFormService(weight, height) {

    const currH = height / 100;
    return weight / (currH * currH);
}
