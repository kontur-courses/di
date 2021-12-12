import React, { Component } from 'react';

import "./TagCloud.css"

export class TagCloud extends Component {
    static displayName = TagCloud.name;
    url = "https://localhost:5001/api/image?text=";
    
    constructor(props) {
        super(props);
        this.state = {
            imageSrc: `Refactoring: Improving the Design of Existing Code
by Martin Fowler, Kent Beck (Contributor), John Brant (Contributor), William
Opdyke, don Roberts
Another stupid release 2002J
For all the people which doesn’t have money to buy a good book
2
Your class library works, but could it be better? Refactoring: Improving the Design of
Existing Code shows how refactoring can make object-oriented code simpler and easier
to maintain. Today refactoring requires considerable design know-how, but once tools
become available, all programmers should be able to improve their code using refactoring
techniques.
Besides an introduction to refactoring, this handbook provides a catalog of dozens of tips
for improving code. The best thing about Refactoring is its remarkably clear presentation,
along with excellent nuts-and-bolts advice, from object expert Martin Fowler. The author
is also an authority on software patterns and UML, and this experience helps make this a
better book, one that should be immediately accessible to any intermediate or advanced
object-oriented developer. (Just like patterns, each refactoring tip is presented with a
simple name, a "motivation," and examples using Java and UML.)
Early chapters stress the importance of testing in successful refactoring. (When you
improve code, you have to test to verify that it still works.) After the discussion on how
to detect the "smell" of bad code, readers get to the heart of the book, its catalog of over
70 "refactorings"--tips for better and simpler class design. Each tip is illustrated with
"before" and "after" code, along with an explanation. Later chapters provide a quick look
at refactoring research.
Like software patterns, refactoring may be an idea whose time has come. This
groundbreaking title will surely help bring refactoring to the programming mainstream.
With its clear advice on a hot new topic, Refactoring is sure to be essential reading for
anyone who writes or maintains object-oriented software. --Richard Dragan
Topics Covered: Refactoring, improving software code, redesign, design tips, patterns,
unit testing, refactoring research, and tools.
Book News, Inc.
A guide to refactoring, the process of changing a software system so that it does not alter
the external behavior of the code yet improves its internal structure, for professional
programmers. Early chapters cover general principles, rationales, examples, and testing.
The heart of the book is a catalog of refactorings, organized in chapters on composing
methods, moving features between objects, organizing data, simplifying conditional
expressions, and dealing with generalizations`,
            value: `Refactoring: Improving the Design of Existing Code
by Martin Fowler, Kent Beck (Contributor), John Brant (Contributor), William
Opdyke, don Roberts
Another stupid release 2002J
For all the people which doesn’t have money to buy a good book
2
Your class library works, but could it be better? Refactoring: Improving the Design of
Existing Code shows how refactoring can make object-oriented code simpler and easier
to maintain. Today refactoring requires considerable design know-how, but once tools
become available, all programmers should be able to improve their code using refactoring
techniques.
Besides an introduction to refactoring, this handbook provides a catalog of dozens of tips
for improving code. The best thing about Refactoring is its remarkably clear presentation,
along with excellent nuts-and-bolts advice, from object expert Martin Fowler. The author
is also an authority on software patterns and UML, and this experience helps make this a
better book, one that should be immediately accessible to any intermediate or advanced
object-oriented developer. (Just like patterns, each refactoring tip is presented with a
simple name, a "motivation," and examples using Java and UML.)
Early chapters stress the importance of testing in successful refactoring. (When you
improve code, you have to test to verify that it still works.) After the discussion on how
to detect the "smell" of bad code, readers get to the heart of the book, its catalog of over
70 "refactorings"--tips for better and simpler class design. Each tip is illustrated with
"before" and "after" code, along with an explanation. Later chapters provide a quick look
at refactoring research.
Like software patterns, refactoring may be an idea whose time has come. This
groundbreaking title will surely help bring refactoring to the programming mainstream.
With its clear advice on a hot new topic, Refactoring is sure to be essential reading for
anyone who writes or maintains object-oriented software. --Richard Dragan
Topics Covered: Refactoring, improving software code, redesign, design tips, patterns,
unit testing, refactoring research, and tools.
Book News, Inc.
A guide to refactoring, the process of changing a software system so that it does not alter
the external behavior of the code yet improves its internal structure, for professional
programmers. Early chapters cover general principles, rationales, examples, and testing.
The heart of the book is a catalog of refactorings, organized in chapters on composing
methods, moving features between objects, organizing data, simplifying conditional
expressions, and dealing with generalizations`
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({value: event.target.value});
    }

    handleSubmit(event) {
        this.setState({imageSrc: this.state.value.replaceAll("\n", " ")})
        event.preventDefault();
    }
    
    render () {
        return (
            <div className="text-input">
                <img className="photo" src={this.url + this.state.imageSrc} alt="альтернативный текст" />
                <form onSubmit={this.handleSubmit}>
                    <textarea cols={100} value={this.state.value} onChange={this.handleChange} />
                    <input type="submit" value="Отправить" />
                </form>
            </div>
        );
    }
}