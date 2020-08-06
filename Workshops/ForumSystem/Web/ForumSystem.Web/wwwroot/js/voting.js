async function sendVote(postId, isUpVote) {
    let csrfTokenElement = document.getElementsByName("__RequestVerificationToken")[0];

    let json = {
        postId: postId,
        isUpVote: isUpVote
    };

    try {

        const appUrl = "https://localhost:5001/api/votes";
        let response = await fetch(appUrl, {
            method: "post",
            headers: {
                'RequestVerificationToken': csrfTokenElement.value,
                'Accept': "application/json",
                'Content-Type': "application/json"
            },
            body: JSON.stringify(json)
        });

        let data = await response.json();

        if (response.ok) {

            let upVoteElement = document.getElementById("up-votes");
            let downVoteElement = document.getElementById("down-votes");
            let votesScoreElement = document.getElementById("votes-score");

            upVoteElement.textContent = data.upVotes;
            downVoteElement.textContent = data.downVotes;
            votesScoreElement.textContent = data.votesScore;
        }

    } catch (err) {
        console.log(err);
    }
}