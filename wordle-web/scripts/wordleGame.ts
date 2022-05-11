import { LetterStatus } from './letter'
import { Word } from './word'
import { WordsService } from './wordsService'

export enum GameState {
  Active = 0,
  Won = 1,
  Lost = 2,
}

export class WordleGame {
  private word: string
  words: Word[] = []
  state: GameState = GameState.Active
  readonly maxGuesses = 6

  constructor(word: string) {
    this.words.push(new Word())
    this.word = word
  }

  get currentWord(): Word {
    return this.words[this.words.length - 1]
  }

  get gameOver(): Boolean {
    return this.state === GameState.Won || this.state === GameState.Lost
  }

  get correctChars() {
    const allLetters = this.words.map((x) => x.letters).flat()
    return allLetters
      .filter((x) => x.status === LetterStatus.Correct)
      .map((x) => x.char)
  }

  get wrongPlaceChars() {
    const allLetters = this.words.map((x) => x.letters).flat()
    const wrongPlaceLetters = allLetters
      .filter((x) => x.status === LetterStatus.WrongPlace)
      .map((x) => x.char)
    return wrongPlaceLetters.filter((x) => !this.correctChars.includes(x))
  }

  get wrongChars() {
    const allLetters = this.words.map((x) => x.letters).flat()
    return allLetters
      .filter((x) => x.status === LetterStatus.Wrong)
      .map((x) => x.char)
  }

  submitWord() {
    if (this.currentWord.evaluateWord(this.word)) {
      this.state = GameState.Won
    } else if (this.words.length === this.maxGuesses) {
      this.state = GameState.Lost
    } else {
      this.words.push(new Word())
    }
  }

  getWildcardWords() {
    const wordList = WordsService.getWildCharacterWords(this.currentWord.text)
    return wordList
  }

  changeCurrentWord(newWord: string) {
    // remove old word
    for (let i = 0; i < newWord.length; i++) {
      this.currentWord.removeLetter()
    }
    // add new word
    for (let i = 0; i < newWord.length; i++) {
      this.currentWord.addLetter(newWord.charAt(i))
    }
  }

  checkIfWordExists(word: string) {
    return WordsService.wordExists(word)
  }
}
