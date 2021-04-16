import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ButtonStatus } from 'src/models/ButtonStatus';
import { StartRequest } from 'src/models/StartRequest';
import { GameResult } from 'src/models/GameResult';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private client: HttpClient) {
    this.StartParams = new StartRequest();
  };

  StartParams: StartRequest;
  public Field: GameResult;

  Start(): void {
    this.client.post<GameResult>("/Api/Game/Start", this.StartParams).subscribe(
      (res) => this.Field = res,
      (err) => console.log(err)
    );
  }
}
