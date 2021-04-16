import { ButtonStatus } from "./ButtonStatus";
import { GameResultEnum } from "./GameResultEnum";

export class GameResult {
    public Result: GameResultEnum;
    public Field: ButtonStatus[][];
    public BombCount: number;
}