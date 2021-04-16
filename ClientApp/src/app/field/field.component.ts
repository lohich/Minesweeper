import { Component, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ButtonStatus } from 'src/models/ButtonStatus';
import { GameResult } from 'src/models/GameResult';
import { GameResultEnum } from 'src/models/GameResultEnum';

@Component({
    selector: 'app-field',
    templateUrl: './field.component.html',
    styleUrls: ['./field.component.css']
})
export class FieldComponent {

    constructor(private client: HttpClient) { };

    @Input() Field: GameResult;

    Open(X: number, Y: number): void {
        this.client.post<GameResult>("/Api/Game/Open", { X, Y }).subscribe(
            (res) => this.Field = res,
            (err) => console.log(err)
        );
    }

    OpenAll(X: number, Y: number): void {
        this.client.post<GameResult>("/Api/Game/OpenAll", { X, Y }).subscribe(
            (res) => this.Field = res,
            (err) => console.log(err)
        );
    }

    Flag(X: number, Y: number): boolean {
        this.client.post<GameResult>("/Api/Game/Flag", { X, Y }).subscribe(
            (res) => this.Field = res,
            (err) => console.log(err)
        );

        return false;
    }
}