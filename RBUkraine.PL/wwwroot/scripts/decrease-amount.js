const decreaseAmount = async (id, onDecrease) => {
    return fetch(`https://localhost:5001/cart/decrease-amount?id=${id}`,
        {
            method: "post"
        }).then(() => onDecrease());
};