const increaseAmount = async (id, onIncrease) => {
    return fetch(`https://localhost:5001/cart/increase-amount?id=${id}`,
        {
            method: "post"
        }).then(() => onIncrease());
};