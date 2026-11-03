\# Maternal Health Risk Predictor (Backpropagation NN)

\## Results and analysis



All numbers below come from the app itself, run on the cleaned data (451 unique patients) with the

stratified 80/20 split, \*\*split seed 1\*\* (361 training rows, 90 test rows). The network is the

professor's backpropagation engine (`NeuralNet`, sigmoid activations, learning rates fixed at 0.2 for

the output layer and 0.15 for the hidden layer), used without changes.



\### 1. Training: the loss goes down



!\[Loss curve](docs/loss\_curve.png)



\*Final model: 6 inputs, 4 hidden nodes, 3 outputs, 2000 epochs. The full per-epoch log is in

\[`training\_log.csv`](training\_log.csv) (exported with \*\*Export loss log (CSV)\*\*).\*



| Epoch | Train MSE | Test MSE |

|---:|---:|---:|

| 0 (random weights) | 0.262 | 0.262 |

| 200 | 0.140 | 0.168 |

| 1000 | 0.119 | 0.161 |

| 2000 | 0.113 | 0.158 |



\- \*\*Backpropagation works.\*\* Training MSE falls from 0.262 to 0.113 (−57%). Most of the drop happens in

&#x20; the first 200 epochs, when the random weights are far from good and every `learn()` call makes large

&#x20; corrections. After that the errors are small, so the updates are small and the curve flattens.

\- \*\*The test loss stops improving early.\*\* It drops with the training loss up to about epoch 200, then

&#x20; only creeps from 0.168 to 0.158 while the training loss keeps falling. The widening gap is mild

&#x20; overfitting: the extra epochs mostly fit the 361 training rows more closely and help the unseen test

&#x20; rows very little.



\### 2. Tuning: hidden nodes × epochs



The \*\*Tuning\*\* tab trained a fresh network \*\*5 times\*\* for every setting (new random weights and a new

shuffle order each time, same split) and reports the mean test accuracy ± standard deviation over the

5 runs. Each network was trained once to 2000 epochs and measured at every listed epoch.



| Hidden \\ Epochs | 10 | 50 | 100 | 500 | 1000 | 2000 |

|---|---:|---:|---:|---:|---:|---:|

| \*\*2\*\* | 55.1 ± 3.0 | 64.4 ± 0.0 | 62.7 ± 1.7 | 62.7 ± 1.3 | 62.9 ± 2.0 | 63.3 ± 1.1 |

| \*\*4\*\* | 60.7 ± 2.3 | 63.8 ± 1.0 | 61.3 ± 1.4 | 67.1 ± 1.7 | 68.0 ± 1.8 | \*\*69.8 ± 2.1\*\* |

| \*\*6\*\* | 61.1 ± 0.8 | 63.6 ± 1.4 | 62.0 ± 1.4 | 67.8 ± 1.4 | 69.6 ± 1.0 | 68.7 ± 0.9 |

| \*\*8\*\* | 61.6 ± 1.0 | 64.0 ± 0.6 | 61.3 ± 1.4 | 68.0 ± 2.8 | 67.6 ± 1.4 | 68.2 ± 2.7 |



\*Test accuracy in %, mean ± std over 5 runs. Train accuracy at 2000 epochs: 72.1% (2 hidden),

75.8% (4), 76.9% (6), 78.3% (8).\*



\*\*What the grid shows\*\*



\- \*\*Chosen setting: 4 hidden nodes, 2000 epochs\*\* (69.8% ± 2.1%), the highest mean. It is

&#x20; \*\*effectively tied\*\* with 6 hidden / 1000 epochs (69.6% ± 1.0%) and with most settings that have

&#x20; 4–8 hidden nodes and 500+ epochs (67–70%). The differences between them are smaller than the

&#x20; run-to-run spread, so the honest conclusion is that \*\*once the network has at least 4 hidden nodes

&#x20; and 500 epochs, its size hardly matters\*\*. We kept the smallest network among the top settings.

\- \*\*2 hidden nodes is too small (underfitting).\*\* It levels off at about 63% no matter how long it

&#x20; trains, and even its training accuracy stays at about 72%. Two hidden neurons cannot separate three

&#x20; classes well.

\- \*\*More hidden nodes means more overfitting.\*\* Training accuracy keeps rising with size (75.8% → 78.3%

&#x20; at 2000 epochs) while test accuracy does not, so the extra capacity memorises training rows instead

&#x20; of learning anything that carries over to new patients.

\- \*\*Epochs matter more than size.\*\* Every network gains about 6–9 points between 10 and 500 epochs.

&#x20; The small dip at 100 epochs, compared with 50, shows up for every size, so it is a phase of training

&#x20; rather than noise from one run.

\- \*\*Why repeated runs:\*\* the engine starts from random weights, so the same setting can score a few

&#x20; points differently from one run to the next. With one run per setting, the "best" row would largely

&#x20; be luck.



\### 3. Evaluation of the final model on the test set



| | |

|---|---|

| \*\*Test accuracy\*\* | \*\*67.8%\*\* (61 of 90 correct) |

| Train accuracy | 77.0% |

| Majority-class baseline (always answer "Low risk") | 52.2% |



This single training run scored 67.8%, inside the 69.8% ± 2.1% range the tuning predicted for the

setting. It beats the baseline by \*\*15.6 percentage points\*\*, so the network has learned real patterns

from the vitals and is not just repeating the most common class.



\*\*Confusion matrix\*\* (rows = actual risk, columns = predicted risk)



| Actual \\ Predicted | Low | Mid | High | Total |

|---|---:|---:|---:|---:|

| \*\*Low\*\* | \*\*43\*\* | 3 | 1 | 47 |

| \*\*Mid\*\* | 11 | \*\*5\*\* | 5 | 21 |

| \*\*High\*\* | 3 | 6 | \*\*13\*\* | 22 |

| \*\*Total predicted\*\* | 57 | 14 | 19 | 90 |



\*\*Per-class metrics\*\*



| Class | Precision | Recall | F1 | Support |

|---|---:|---:|---:|---:|

| Low risk | 0.754 | 0.915 | 0.827 | 47 |

| Mid risk | 0.357 | 0.238 | 0.286 | 21 |

| High risk | 0.684 | 0.591 | 0.634 | 22 |

| \*\*Macro average\*\* | \*\*0.599\*\* | \*\*0.581\*\* | \*\*0.582\*\* | 90 |



\*\*Reading the results\*\*



\- \*\*Low risk is learned well\*\* (recall 0.915: 43 of 47 found). It is the largest class (52% of the

&#x20; data) and has the most typical vitals: normal blood pressure and blood sugar.

\- \*\*Mid risk is the weak point\*\* (recall 0.238: only 5 of 21 found). Its vitals sit between the other

&#x20; two classes, so 11 mid-risk patients were called Low and 5 were called High. Two facts from the data

&#x20; explain this. First, mid risk is the smallest class after cleaning (106 rows). Second, the data

&#x20; contains \*\*35 sets of identical vitals that carry different risk labels\*\*, so no model could

&#x20; classify all of those rows correctly.

\- \*\*High risk: 13 of 22 found (recall 0.591).\*\* In a medical setting this is the number that matters

&#x20; most, because a missed high-risk mother is the dangerous mistake. 6 high-risk patients were called

&#x20; Mid, which is a partial miss, but \*\*3 were called Low risk, which is the most serious error\*\* in the

&#x20; matrix. Only 1 low-risk patient was flagged as High, so the model leans towards under-warning rather

&#x20; than over-warning.

\- \*\*The macro average (F1 0.582) is lower than the accuracy (67.8%)\*\* because it gives the small Mid

&#x20; class as much weight as Low. The accuracy is pulled up by the large, easy Low class.



\### 4. Limitations



\- \*\*The test set is small.\*\* With 90 test rows, one patient is 1.1 percentage points, so differences

&#x20; of 1–2 points between settings are not meaningful.

\- \*\*Settings were chosen on the test set.\*\* We picked the best setting by its test accuracy, so the

&#x20; reported score is slightly optimistic. A stricter setup would tune on a separate validation split (or

&#x20; use cross-validation) and touch the test set only once. With only 451 unique rows we kept a plain

&#x20; 80/20 split and state this openly.

\- \*\*The engine's learning rates are fixed\*\* (0.2 / 0.15, `const` in `BackNeural.cs`). Since the

&#x20; activity requires using the given classes unchanged, learning rate could not be tuned, only hidden

&#x20; nodes and epochs.

\- \*\*Duplicates.\*\* 562 of the 1,014 original rows were exact copies. We removed them \*\*before\*\*

&#x20; splitting so no test row also appears in training. With duplicates kept, test accuracy would look

&#x20; higher, but it would partly measure memorisation. (See the Preprocessing section.)

\- \*\*Not a medical tool.\*\* This is a class exercise on a small public dataset. Its 59% recall for high

&#x20; risk is far below what a real screening tool would need.



\### 5. How to reproduce these numbers



1\. \*\*Load CSV\*\* → `Maternal\_Health\_Risk\_Data\_Set.csv`.

2\. Keep Training % \*\*80\*\*, Split seed \*\*1\*\*, and all three checkboxes ticked, then click

&#x20;  \*\*Clean + Split + Normalize\*\*.

3\. \*\*Tuning tab:\*\* Hidden sizes `2, 4, 6, 8`, Epoch counts `10, 50, 100, 500, 1000, 2000`, Runs per

&#x20;  config `5` → \*\*Run grid search\*\*.

4\. \*\*Final model:\*\* Hidden nodes \*\*4\*\*, Epochs \*\*2000\*\* → \*\*Create NN\*\* → \*\*Train NN\*\* →

&#x20;  \*\*Evaluate on test set\*\* → \*\*Export loss log (CSV)\*\*.



The engine's random starting weights are not seeded, so your numbers will differ slightly from run to

run (typically within the ± std shown in the tuning table). The split itself is fixed by the seed.

